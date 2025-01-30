using System.Reflection;
using System.Security.Claims;
using Elastic.Channels;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SelfWaiter.DealerAPI.Core.Application.Behaviors;
using SelfWaiter.DealerAPI.Core.Application.Behaviors.Dispatchers;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Infrastructure.ExceptionHandling;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.Repositories;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;
using Serilog;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

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
            services.AddScoped<IUserProfileAndDealerRepository, UserProfileAndDealerRepository>();
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

        public static IServiceCollection AddDealerCache(this IServiceCollection services, IConfiguration configuration, bool enableRedis = false)
        {

            var builder = services.AddFusionCache()
                .WithDefaultEntryOptions(options =>
                {
                    options.Duration = TimeSpan.FromMinutes(10);
                    options.DistributedCacheDuration = TimeSpan.FromMinutes(10);
                })
                .WithSerializer(new FusionCacheSystemTextJsonSerializer());
                
            if (enableRedis)
            {
                builder.WithDistributedCache(
                        new RedisCache(new RedisCacheOptions() { Configuration = configuration.GetConnectionString("Redis") })
                    );
            }

            builder.AsHybridCache();

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

        public static IServiceCollection AddDealerHealthChecks(this IServiceCollection services, IConfiguration configuration, bool enableMSSQL = true, bool enableRedis = false, bool enableElasticsearch = false)
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
                            tags: new [] {"MSSQL"}
                        );
                }
            }

            if (enableRedis)
            {
                string? connectionString = configuration.GetConnectionString("Redis");
                if (!string.IsNullOrEmpty(connectionString))
                {
                    //ConnectionMultiplexer.Connect(connectionString)
                    builder.AddRedis(
                            redisConnectionString: connectionString,
                            name: "Redis Check",
                            failureStatus: HealthStatus.Degraded | HealthStatus.Unhealthy,
                            tags: new[] { "Redis" }
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
                if(claimsPrincipal.Identity?.IsAuthenticated != true)
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
