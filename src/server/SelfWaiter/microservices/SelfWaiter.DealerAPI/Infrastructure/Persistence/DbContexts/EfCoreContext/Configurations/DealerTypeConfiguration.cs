using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfWaiter.DealerAPI.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext.Configurations
{
    public class DealerTypeConfiguration : IEntityTypeConfiguration<Dealer>
    {
        public void Configure(EntityTypeBuilder<Dealer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100);

            builder.Property(x => x.Adress)
            .HasMaxLength(250);

            builder
                .HasOne(d => d.CreatorUser)
                .WithMany(u => u.CreatedDealers)
                .HasForeignKey(d => d.CreatorUserId)
                .OnDelete(DeleteBehavior.SetNull);

        }

        private IEnumerable<Dealer> SeedDatas()
        {
            yield break;
        }
    }
}
