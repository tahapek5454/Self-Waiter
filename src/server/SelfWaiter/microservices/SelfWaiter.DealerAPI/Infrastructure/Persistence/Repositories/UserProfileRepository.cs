using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;

namespace SelfWaiter.DealerAPI.Infrastructure.Persistence.Repositories
{
    public class UserProfileRepository(AppDbContext _appDbContext) : BaseRepository<UserProfile>(_appDbContext), IUserProfileRepository
    {
        public DbSet<UserProfile> Table => _appDbContext.Set<UserProfile>();
    }
}
