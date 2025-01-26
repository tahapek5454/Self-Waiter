using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfWaiter.DealerAPI.Core.Domain.Entities;
using SelfWaiter.Shared.Core.Application.Utilities.Consts;

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
                Id = Guid.Parse(SelfWaiterDefaultValues.UserId),
                Name = "Taha",
                Surname = "Pek",
                Email = "taha_test@gmail.com",
                UserName = "taha.pek",
                CreatedDate = new DateTime(2025,01,25),
                CreatorUserName = "taha.pek"
            };

            yield return new()
            {
                Id = Guid.Parse(SelfWaiterDefaultValues.UserId2),
                Name = "Ahmet Zeyt",
                Surname = "Sertel",
                Email = "ahmet_test@gmail.com",
                UserName = "azeyt.sertel",
                CreatedDate = new DateTime(2025, 01, 25),
                CreatorUserName = "taha.pek"
            };
        }
    }
}
