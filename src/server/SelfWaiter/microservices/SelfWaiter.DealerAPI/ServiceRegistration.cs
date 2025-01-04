using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;

namespace SelfWaiter.DealerAPI
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDealerService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MSSQL"));
            });

            return services;
        }
    }
}
