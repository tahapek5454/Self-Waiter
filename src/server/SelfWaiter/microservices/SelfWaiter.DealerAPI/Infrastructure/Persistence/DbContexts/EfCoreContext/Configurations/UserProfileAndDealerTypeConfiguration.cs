using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfWaiter.DealerAPI.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext.Configurations
{
    public class UserProfileAndDealerTypeConfiguration : IEntityTypeConfiguration<UserProfileAndDealer>
    {
        public void Configure(EntityTypeBuilder<UserProfileAndDealer> builder)
        {
            
        }

        private IEnumerable<UserProfileAndDealer> SeedDatas()
        {
            yield break;
        }
    }
}
