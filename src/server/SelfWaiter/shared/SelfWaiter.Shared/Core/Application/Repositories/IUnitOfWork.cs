using SelfWaiter.Shared.Core.Domain.Entities;

namespace SelfWaiter.Shared.Core.Application.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        int SaveChange();
        Task<IEnumerable<IEntity>> GetChangedEntitiesAsync();
        Task<IEnumerable<IEntity>> GetEntitiesAsync();
    }
}
