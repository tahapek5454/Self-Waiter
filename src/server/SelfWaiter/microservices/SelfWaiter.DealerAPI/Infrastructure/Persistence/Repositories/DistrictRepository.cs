using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;

namespace SelfWaiter.DealerAPI.Infrastructure.Persistence.Repositories
{
    public class DistrictRepository(AppDbContext _appDbContext) : BaseRepository<District>(_appDbContext), IDistrictRepository
    {
        public DbSet<District> Table { get => _appDbContext.Set<District>(); }
    }
}
