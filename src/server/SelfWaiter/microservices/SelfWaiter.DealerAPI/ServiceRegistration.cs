using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Behaviors;
using SelfWaiter.DealerAPI.Core.Application.Behaviors.Dispatchers;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Infrastructure.ExceptionHandling;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.Repositories;

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
    }
}
