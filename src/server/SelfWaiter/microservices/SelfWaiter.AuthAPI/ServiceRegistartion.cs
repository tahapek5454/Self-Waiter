using Microsoft.EntityFrameworkCore;
using SelfWaiter.AuthAPI.Core.Domain.Entities;
using SelfWaiter.AuthAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;

namespace SelfWaiter.AuthAPI
{
    public static class ServiceRegistartion
    {
        public static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString("MSSQL"));
            });

            services.AddIdentityApiEndpoints<AppUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<AppDbContext>();

            return services;
        }
    }
}
