using System.Linq.Expressions;
using SelfWaiter.Shared.Core.Domain.Entities;

namespace SelfWaiter.Shared.Core.Application.Repositories
{
    public interface IBaseRepository<T> where T : class, IEntity
    {
        Task<List<T>> GetAllAsync(bool tracking = true);
        Task<T?> GetByIdAsync(Guid id, bool tracking = true);
        IQueryable<T> Where(Expression<Func<T, bool>> exp);
        IQueryable<T> Query();
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> exp, bool tracking = true);
        Task<bool> AnyAsync(Expression<Func<T, bool>> exp);

        Task CreateAsync(T entity);
        Task CreateRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);
    }
}
