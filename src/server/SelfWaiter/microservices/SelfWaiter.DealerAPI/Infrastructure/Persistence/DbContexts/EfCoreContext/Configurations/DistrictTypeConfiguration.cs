using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfWaiter.DealerAPI.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext.Configurations
{
    public class DistrictTypeConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(80);

            builder.HasIndex(x => new { x.Name, x.IsValid })
                .IsUnique();

            builder.HasIndex(x => x.IsValid);

            builder.HasData(SeedDatas());
        }


        private IEnumerable<District> SeedDatas()
        {
            yield return new District()
            {
                Id = new Guid("24818128-6c4e-453d-8a1f-15f75e8aa746"),
                CityId = new Guid("7333D5E7-AE6A-4523-88AC-7383C9A9F6A5"),
                CreatedDate = new DateTime(2025, 01, 01),
                CreatorUserName = "tahapek",
                Name = "Serdivan"
            };
        }
    }

}
