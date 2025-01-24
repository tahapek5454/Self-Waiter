using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfWaiter.DealerAPI.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext.Configurations
{
    public class CountryTypeConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(80);

            builder.HasData(SeedDatas());
        }

        private IEnumerable<Country> SeedDatas()
        {
            yield return new Country()
            {
                CreatedDate = new DateTime(2025, 01, 01),
                CreatorUserName = "tahapek",
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Name = "Türkiye",
            };
        }
    }
}
