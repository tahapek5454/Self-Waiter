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
                .HasMaxLength(75)
                .IsRequired();

            builder.Property(x => x.Adress)
            .HasMaxLength(250);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(25);

            builder.HasIndex(x => x.PhoneNumber)
                .IsUnique();

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.HasIndex(x => x.IsValid)
                .IsDescending(true);

            builder
                .HasOne(d => d.CreatorUser)
                .WithMany(u => u.CreatedDealers)
                .HasForeignKey(d => d.CreatorUserId)
                .OnDelete(DeleteBehavior.SetNull);


            builder
                .HasMany(x => x.DealerImages)
                .WithOne(y => y.Dealer)
                .HasForeignKey(dealerImage => dealerImage.RelationId);

        }

        private IEnumerable<Dealer> SeedDatas()
        {
            yield break;
        }
    }
}
