using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;
using SelfWaiter.Shared.Core.Application.Extension;
using SelfWaiter.Shared.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Infrastructure.Persistence.Repositories
{
    public abstract class BaseRepository<T>(AppDbContext _appDbContext) : IBaseRepository<T> where T : class, IEntity
    {
        
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> exp)
        {
            return await _appDbContext.Set<T>().AnyAsync(exp);
        }

        public async Task CreateAsync(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
        }

        public async Task CreateRangeAsync(IEnumerable<T> entities)
        {
            await _appDbContext.Set<T>().AddRangeAsync(entities);
        }

        public Task DeleteAsync(T entity)
        {
            entity.IsValid = false;

            return Task.CompletedTask;
        }

        public  Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            entities.Foreach(x =>x.IsValid=false);
            return Task.CompletedTask;
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> exp, bool tracking = true)
        {
            if (tracking)
            {
                return await _appDbContext.Set<T>().FirstOrDefaultAsync(exp);
            }

            return await _appDbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(exp);
        }

        public async Task<List<T>> GetAllAsync(bool tracking = true)
        {
            if (tracking)
            {
                return await _appDbContext.Set<T>().ToListAsync();
            }

            return await _appDbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id, bool tracking = true)
        {
            if (tracking)
            {
                return await _appDbContext.Set<T>().FirstOrDefaultAsync(x => x.Id.Equals(id));
            }

            return await _appDbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public IQueryable<T> Query()
        {
            return _appDbContext.Set<T>().AsQueryable();
        }

        public Task UpdateAsync(T entity)
        {
            return Task.Run(() =>
            {
                _appDbContext.Set<T>().Update(entity);
            });
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> exp)
        {
            return _appDbContext.Set<T>().Where(exp);
        }
    }
}
