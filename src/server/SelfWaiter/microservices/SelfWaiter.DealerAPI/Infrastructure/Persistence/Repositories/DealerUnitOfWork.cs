using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;
using SelfWaiter.Shared.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Infrastructure.Persistence.Repositories
{
    public class DealerUnitOfWork(AppDbContext _appDbContext) : IDealerUnitOfWork
    {
        public async Task<IEnumerable<IEntity>> GetChangedEntitiesAsync()
        {
            var changedEntities = _appDbContext.ChangeTracker.Entries<IEntity>()
                                                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added || e.State == EntityState.Deleted)
                                                .Select(e => e.Entity)
                                                .ToList();

            return await Task.FromResult(changedEntities);
        }

        public async Task<IEnumerable<IEntity>> GetEntitiesAsync()
        {
            var changedEntities = _appDbContext.ChangeTracker.Entries<IEntity>()
                                                .Select(e => e.Entity)
                                                .ToList();

            return await Task.FromResult(changedEntities);
        }

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
