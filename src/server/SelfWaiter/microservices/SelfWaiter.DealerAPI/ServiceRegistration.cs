using MediatR;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Behaviors;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.Repositories;
using SelfWaiter.DealerAPI.Core.Application.Services;
using SelfWaiter.DealerAPI.Infrastructure.InnerInfrastructure.Services;

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
            });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(BeforeHandlerBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AfterHandlerBehavior<,>));
            #endregion

            #region Repositories
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IDealerRepository, DealerRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            #endregion

            #region Services
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IDealerService, DealerService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            #endregion

            return services;
        }
    }
}
