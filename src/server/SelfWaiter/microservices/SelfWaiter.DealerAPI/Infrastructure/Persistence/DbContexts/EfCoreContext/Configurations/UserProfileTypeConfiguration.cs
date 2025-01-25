using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfWaiter.DealerAPI.Core.Domain.Entities;

namespace SelfWaiter.DealerAPI.Infrastructure.Persistence.DbContexts.EfCoreContext.Configurations
{
    public class UserProfileTypeConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(x => x.Id);  

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Surname)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(75);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(25);

            builder.HasIndex(x => x.IsValid)
                .IsDescending(true);

            builder.HasIndex(x => x.UserName)
                .IsUnique();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.HasIndex(x => x.PhoneNumber)
                .IsUnique();

            builder.HasData(SeedDatas());
        }

        private IEnumerable<UserProfile> SeedDatas()
        {
            yield return new()
            {
                Id = Guid.Parse("2d9b274f-753f-4aab-947d-cbf9d232b811"),
                Name = "Taha",
                Surname = "Pek",
                Email = "taha_test@gmail.com",
                UserName = "taha.pek",
                CreatedDate = new DateTime(2025,01,25),
                CreatorUserName = "taha.pek"
            };
        }
    }
}
