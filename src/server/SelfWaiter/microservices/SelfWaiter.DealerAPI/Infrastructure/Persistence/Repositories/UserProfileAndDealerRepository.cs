using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;

namespace SelfWaiter.DealerAPI.Infrastructure.Persistence.Repositories
{
    public class UserProfileAndDealerRepository : BaseRepository<UserProfileAndDealer>, IUserProfileAndDealerRepository
    {
        public UserProfileAndDealerRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }
    }
}
