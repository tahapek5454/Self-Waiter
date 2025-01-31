using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfWaiter.FileAPI.Core.Domain;

namespace SelfWaiter.FileAPI.Infrastructure.Persistence.DbContexts.EfCoreContext.Configurations
{
    public class DealerImageFileTypeConfiguration : IEntityTypeConfiguration<DealerImageFile>
    {
        public void Configure(EntityTypeBuilder<DealerImageFile> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.CreatedDate)
                .IsDescending(true);

            builder.HasIndex(x => new { x.RelationId, x.CreatedDate })
                .IsDescending(true, true);

        }
    }
}
