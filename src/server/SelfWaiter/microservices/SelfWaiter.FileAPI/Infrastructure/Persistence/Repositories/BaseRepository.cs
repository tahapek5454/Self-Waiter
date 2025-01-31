using Microsoft.EntityFrameworkCore;
using SelfWaiter.FileAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;
using SelfWaiter.Shared.Core.Application.Repositories;
using SelfWaiter.Shared.Core.Domain.Entities;
using System.Linq.Expressions;
using System.Reflection;

namespace SelfWaiter.FileAPI.Infrastructure.Persistence.Repositories
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
            _appDbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _appDbContext.Set<T>().RemoveRange(entities);
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

        public void UpdateAdvance(T entity, object obj)
        {
            var requestProperties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
           .Where(prop => prop.Name != nameof(entity.Id));

            bool isUpdate = false;
            foreach (var requestProperty in requestProperties)
            {
                var value = requestProperty.GetValue(obj);
                if (value != null)
                {
                    var entityProperty = entity.GetType().GetProperty(requestProperty.Name);
                    if (entityProperty != null && entityProperty.CanWrite)
                    {
                        entityProperty.SetValue(entity, value);
                        isUpdate = true;
                    }
                }
            }

            if (isUpdate)
                _appDbContext.Set<T>().Update(entity);

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
