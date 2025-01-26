using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfWaiter.DealerAPI.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext.Configurations
{
    public class UserProfileAndDealerTypeConfiguration : IEntityTypeConfiguration<UserProfileAndDealer>
    {
        public void Configure(EntityTypeBuilder<UserProfileAndDealer> builder)
        {
            builder.HasIndex(x => new { x.DealerId, x.UserProfileId, x.IsValid })
                .IsUnique();
        }

        private IEnumerable<UserProfileAndDealer> SeedDatas()
        {
            yield break;
        }
    }
}
