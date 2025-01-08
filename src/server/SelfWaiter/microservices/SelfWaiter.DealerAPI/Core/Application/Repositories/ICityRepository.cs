using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Application.Repositories;

namespace SelfWaiter.DealerAPI.Core.Application.Repositories
{
    public interface ICityRepository: IBaseRepository<City>
    {
        public DbSet<City> Table { get; }
    }
}
