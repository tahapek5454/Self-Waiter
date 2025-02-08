using Elastic.Channels;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SelfWaiter.FileAPI.Core.Application.Behaviors;
using SelfWaiter.FileAPI.Core.Application.Behaviors.Dispatchers;
using SelfWaiter.FileAPI.Core.Application.Repositories;
using SelfWaiter.FileAPI.Core.Application.Services.Storage;
using SelfWaiter.FileAPI.Infrastructure.ExceptionHandling;
using SelfWaiter.FileAPI.Infrastructure.InnerInfrastructure.Consumers.DealerImageFileChangedConsumers;
using SelfWaiter.FileAPI.Infrastructure.InnerInfrastructure.Services.Storages;
using SelfWaiter.FileAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;
using SelfWaiter.FileAPI.Infrastructure.Persistence.Repositories;
using SelfWaiter.Shared.Core.Application.Services;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using SelfWaiter.Shared.Infrastructure.InnerInfrastructure.Services;
using Serilog;
using System.Reflection;
using System.Security.Claims;

namespace SelfWaiter.FileAPI
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddFileService(this IServiceCollection services, IConfiguration configuration)
        {

            #region DbContext
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MSSQL"));
            });
            #endregion

            #region Mediatr
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                configuration.AddOpenBehavior(typeof(BeforeHandlerBehavior<,>));
                configuration.AddOpenBehavior(typeof(AfterHandlerBehavior<,>));
            });
            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
            #endregion

            #region Storage Services
            services.AddScoped<IStorageService, StorageService>();
            #endregion

            #region Repositroies
            services.AddScoped<IDealerImageFileRepository, DealerImageFileRepository>();
            services.AddScoped<IFileUnitOfWork, FileUnitOfWork>();
            #endregion

            #region Validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            #endregion

            #region Exceptions
            services.AddExceptionHandler<SelfWaiterFileValidationExceptionHandler>();
            services.AddExceptionHandler<SelfWaiterFileExceptionHandler>();
            services.AddExceptionHandler<FileExceptionHandler>();
            services.AddProblemDetails();
            #endregion

            #region MassTransit
            services.AddMassTransit(configure =>
            {

                configure.AddConsumer<DealerImageFileRollbackEventConsumer>();
                configure.AddConsumer<DealerImageFileDeleteEventConsumer>();


                configure.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(configuration.GetConnectionString("RabbitMQ"));

                    configurator.ReceiveEndpoint(RabbitMQSettings.File_DealerImageFileNotReceivedQueue, e =>
                    {
                        e.ConfigureConsumer<DealerImageFileRollbackEventConsumer>(context);
                        e.DiscardSkippedMessages();
                    });

                    configurator.ReceiveEndpoint(RabbitMQSettings.File_DealerImageFileDeleteQueue, e =>
                    {
                        e.ConfigureConsumer<DealerImageFileDeleteEventConsumer>(context);
                        e.DiscardSkippedMessages();
                    });
                });
            });

            services.AddScoped<IIntegrationBus, IntegrationBus>();
            #endregion

            return services;
        }

        public static IServiceCollection AddStorage<T>(this IServiceCollection services)
          where T : Storage, IStorage
        {
            services.AddScoped<IStorage, T>();

            return services;
        }

        public static IHostBuilder AddFileLoggerService(this IHostBuilder hostBuilder, bool enableElasticsearch = false)
        {
            hostBuilder.UseSerilog((context, configuration) =>
            {
                var loggerConfiguration = configuration.Enrich.FromLogContext()
                .ReadFrom.Configuration(context.Configuration);

                if (enableElasticsearch)
                {
                    loggerConfiguration.WriteTo.Elasticsearch(new[] { new Uri(context.Configuration["URLS:ElasticSearch"] ?? "http://localhost:9200") }, opts =>
                    {
                        opts.DataStream = new DataStreamName("logs", "file-service");
                        opts.BootstrapMethod = BootstrapMethod.Failure;
                        opts.ConfigureChannel = channelOpts =>
                        {
                            channelOpts.BufferOptions = new BufferOptions { ExportMaxConcurrency = 10 };
                        };
                    }, transport =>
                    {
                        // ForNow we dont have useName and  password development env
                        //transport.Authentication(new BasicAuthentication(username, password)); 
                    });
                }
            });

            return hostBuilder;
        }

        public static IServiceCollection AddFileHealthChecks(this IServiceCollection services, IConfiguration configuration, bool enableMSSQL = true, bool enableElasticsearch = false)
        {
            var builder = services.AddHealthChecks();

            if (enableMSSQL)
            {
                string? connectionString = configuration.GetConnectionString("MSSQL");
                if (!string.IsNullOrEmpty(connectionString))
                {
                    builder.AddSqlServer(
                            connectionString: connectionString,
                            name: "MSSQL Check",
                            failureStatus: HealthStatus.Degraded | HealthStatus.Unhealthy,
                            tags: new[] { "MSSQL" }
                        );
                }
            }

            if (enableElasticsearch)
            {
                string? connectionString = configuration["URLS:ElasticSearch"];

                if (!string.IsNullOrEmpty(connectionString))
                {
                    builder.AddElasticsearch(
                            elasticsearchUri: connectionString,
                            name: "ElasticSearch Check",
                            failureStatus: HealthStatus.Degraded | HealthStatus.Unhealthy,
                            tags: new[] { "ElasticSearch" }
                        );
                    //builder.AddUrlGroup(new Uri(connectionString), name: "elasticsearch", tags: new[] { "elasticsearch" });
                }
            }

            return services;
        }


        public static Guid? GetUserIdOrDefault(this ClaimsPrincipal claimsPrincipal, bool IsDevelopment = false)
        {
            if (!IsDevelopment)
            {
                if (claimsPrincipal.Identity?.IsAuthenticated != true)
                {
                    return null;
                }
                else
                {
                    return GetUserId(claimsPrincipal);
                }
            }
            else
            {
                Guid? userId = GetUserId(claimsPrincipal);

                if (userId is null)
                    return Guid.Parse(SelfWaiterDefaultValues.UserId);
                else
                    return userId;
            }
        }

        private static Guid? GetUserId(ClaimsPrincipal claimsPrincipal)
        {
            string? userId = claimsPrincipal.Claims
                                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
                userId = claimsPrincipal.Claims
                        .FirstOrDefault(c => c.Type == SelfWaiterDefaultValues.AlternativeUserIdClaimType)?.Value;

            if (Guid.TryParse(userId, out Guid guidId))
                return guidId;
            else return null;
        }
    }
}
