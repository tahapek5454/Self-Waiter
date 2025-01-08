using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;

namespace SelfWaiter.DealerAPI.Infrastructure.Persistence.Repositories
{
    public class CityRepository(AppDbContext _appDbContext) : BaseRepository<City>(_appDbContext), ICityRepository
    {
        public DbSet<City> Table { get => _appDbContext.Set<City>(); }
    }
}
