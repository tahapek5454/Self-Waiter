using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Application.Repositories;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext;

namespace SelfWaiter.DealerAPI.Infrastructure.Persistence.Repositories
{
    public class DealerRepository(AppDbContext _appDbContext) : BaseRepository<Dealer>(_appDbContext), IDealerRepository
    {
        public DbSet<Dealer> Table { get => _appDbContext.Set<Dealer>(); }
    }
}
