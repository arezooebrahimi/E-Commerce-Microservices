using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Data.Configurations
{
    public class CategoryMediaEntityConfiguration : IEntityTypeConfiguration<CategoryMedia>
    {
        public void Configure(EntityTypeBuilder<CategoryMedia> builder)
        {

            builder.HasKey(pc => new { pc.MediaId, pc.CategoryId });

            builder.HasOne(pc => pc.Category)
                   .WithMany(p => p.Medias)
                   .HasForeignKey(pc => pc.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
