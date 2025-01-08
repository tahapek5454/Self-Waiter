using System.Linq.Expressions;
using SelfWaiter.Shared.Core.Domain.Dtos;
using SelfWaiter.Shared.Core.Domain.Entities;

namespace SelfWaiter.Shared.Core.Application.Services
{
    public interface IBaseService <D, T> where D:class,IEntity where T : class,IDto
    {
        Task<List<T>> GetAllAsync(bool tracking = true);
        Task<T?> GetByIdAsync(Guid id, bool tracking = true);
        IQueryable<T> Where(Expression<Func<D, bool>> exp);
        Task<T?> FirstOrDefaultAsync(Expression<Func<D, bool>> exp, bool tracking = true);
        Task<bool> AnyAsync(Expression<Func<D, bool>> exp);

        Task CreateAsync(T dto);
        Task UpdateAsync(T dto);
        Task DeleteAsync(T dto);
        Task RemoveAsync(T dto);
    }
}
