namespace SelfWaiter.Shared.Core.Application.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        int SaveChange();
    }
}
