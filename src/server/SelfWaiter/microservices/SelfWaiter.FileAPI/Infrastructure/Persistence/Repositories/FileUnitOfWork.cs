using Microsoft.EntityFrameworkCore;
using SelfWaiter.FileAPI.Core.Application.Repositories;
using SelfWaiter.FileAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;
using SelfWaiter.Shared.Core.Domain.Entities;

namespace SelfWaiter.FileAPI.Infrastructure.Persistence.Repositories
{
    public class FileUnitOfWork(AppDbContext _appDbContext) : IFileUnitOfWork
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
