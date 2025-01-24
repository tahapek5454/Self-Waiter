using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfWaiter.DealerAPI.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext.Configurations
{
    public class CityTypeConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
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

        private IEnumerable<City> SeedDatas()
        {
            var cities = new List<City>()
            {
                new()
                {
                    CountryId = new Guid("00000000-0000-0000-0000-000000000001"),
                    CreatedDate = new DateTime(2025,01,01),
                    CreatorUserName = "tahapek",
                    Id = new Guid("7333d5e7-ae6a-4523-88ac-7383c9a9f6a5"),
                    Name = "Sakarya", 
                }
            };
            


            return cities;
        }
    }

   
}
