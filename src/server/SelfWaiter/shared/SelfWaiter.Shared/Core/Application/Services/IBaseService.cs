using System.Linq.Expressions;
using SelfWaiter.Shared.Core.Domain.Dtos;
using SelfWaiter.Shared.Core.Domain.Entities;

namespace SelfWaiter.Shared.Core.Application.Services
{
    public interface IBaseService <D, T> where D:class,IEntity where T : class,IDto
    {
        Task<List<T>> GetAllAsync(bool tracking = true);
        Task<T> GetByIdAsync(int id, bool tracking = true);
        IQueryable<T> Where(Expression<Func<D, bool>> exp);
        Task<T> FirstOrDefaultAsync(Expression<Func<D, bool>> exp, bool tracking = true);
        bool AnyAsync(Expression<Func<D, bool>> exp);

        Task<string> CreateAsync(T entity);
        Task<string> UpdateAsync(T entity);
        Task<string> DeleteAsync(T entity);
        Task<string> RemoveAsync(T entity);
    }
}
