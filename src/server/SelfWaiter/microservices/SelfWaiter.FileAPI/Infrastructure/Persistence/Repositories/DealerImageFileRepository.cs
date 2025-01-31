using SelfWaiter.FileAPI.Core.Application.Repositories;
using SelfWaiter.FileAPI.Core.Domain;
using SelfWaiter.FileAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;

namespace SelfWaiter.FileAPI.Infrastructure.Persistence.Repositories
{
    public class DealerImageFileRepository : BaseRepository<DealerImageFile>, IDealerImageFileRepository
    {
        public DealerImageFileRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }
    }
}
