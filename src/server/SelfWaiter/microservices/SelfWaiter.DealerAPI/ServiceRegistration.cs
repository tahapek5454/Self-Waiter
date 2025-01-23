using System.Reflection;
using Elastic.Channels;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Behaviors;
using SelfWaiter.DealerAPI.Core.Application.Behaviors.Dispatchers;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Infrastructure.ExceptionHandling;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.Repositories;
using Serilog;

namespace SelfWaiter.DealerAPI
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDealerService(this IServiceCollection services, IConfiguration configuration)
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
            services.AddScoped<IDomainEventsDispatcher,  DomainEventsDispatcher>();
            #endregion

            #region Repositories
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IDealerRepository, DealerRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IDealerUnitOfWork, DealerUnitOfWork>();
            #endregion

            #region Validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            #endregion

            #region Exceptions
            services.AddExceptionHandler<SelfWaiterDealerValidationExceptionHandler>();
            services.AddExceptionHandler<SelfWaiterDealerExceptionHandler>();
            services.AddExceptionHandler<DealerExceptionHandler>();
            services.AddProblemDetails();
            #endregion
            return services;
        }

        public static IHostBuilder AddDealerLoggerService(this IHostBuilder hostBuilder, bool enableElasticsearch = false)
        {
            hostBuilder.UseSerilog((context, configuration) =>
            {
                var loggerConfiguration = configuration.Enrich.FromLogContext()
                .ReadFrom.Configuration(context.Configuration);

                if (enableElasticsearch)
                {
                    loggerConfiguration.WriteTo.Elasticsearch(new[] { new Uri(context.Configuration["URLS:ElasticSearch"] ?? "http://localhost:9200") }, opts =>
                    {
                        opts.DataStream = new DataStreamName("logs", "dealer-service");
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
    }
}
