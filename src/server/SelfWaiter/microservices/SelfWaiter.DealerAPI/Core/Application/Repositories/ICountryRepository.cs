using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Application.Repositories;

namespace SelfWaiter.DealerAPI.Core.Application.Repositories
{
    public interface ICountryRepository: IBaseRepository<Country>
    {
        public DbSet<Country> Table { get; }
    }
}
