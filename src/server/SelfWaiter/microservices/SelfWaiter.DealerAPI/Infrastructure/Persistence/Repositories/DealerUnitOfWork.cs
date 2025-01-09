using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;

namespace SelfWaiter.DealerAPI.Infrastructure.Persistence.Repositories
{
    public class DealerUnitOfWork(AppDbContext _appDbContext) : IDealerUnitOfWork
    {
        public int SaveChange()
        {
            return _appDbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();  
        }
    }
}
