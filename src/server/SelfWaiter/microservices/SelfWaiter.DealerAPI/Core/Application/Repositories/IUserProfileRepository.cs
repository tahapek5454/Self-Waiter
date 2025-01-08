using Microsoft.EntityFrameworkCore;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Application.Repositories;

namespace SelfWaiter.DealerAPI.Core.Application.Repositories
{
    public interface IUserProfileRepository: IBaseRepository<UserProfile>
    {
        public DbSet<UserProfile> Table { get; }
    }
}
