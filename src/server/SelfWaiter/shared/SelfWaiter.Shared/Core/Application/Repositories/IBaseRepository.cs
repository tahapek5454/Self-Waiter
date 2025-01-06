using System.Linq.Expressions;
using SelfWaiter.Shared.Core.Domain.Entities;

namespace SelfWaiter.Shared.Core.Application.Repositories
{
    public interface IBaseRepository<T> where T : class, IEntity
    {
        Task<List<T>> GetAllAsync(bool tracking = true);
        Task<T> GetByIdAsync(int id, bool tracking = true);
        IQueryable<T> Where(Expression<Func<T, bool>> exp);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> exp, bool tracking = true);
        Task<bool> AnyAsync(Expression<Func<T, bool>> exp);

        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
