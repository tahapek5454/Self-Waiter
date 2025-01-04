using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.AuthAPI.Core.Domain.Entities;

namespace SelfWaiter.AuthAPI.Infrastructure.Persistence.DbContexts.EfCoreContext
{
    public class AppDbContext: IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
