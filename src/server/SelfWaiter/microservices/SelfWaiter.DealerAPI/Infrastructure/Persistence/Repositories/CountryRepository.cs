using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;

namespace SelfWaiter.DealerAPI.Infrastructure.Persistence.Repositories
{
    public class CountryRepository(AppDbContext _appDbContext) : BaseRepository<Country>(_appDbContext), ICountryRepository
    {
        public DbSet<Country> Table { get => _appDbContext.Set<Country>(); }
    }
}
