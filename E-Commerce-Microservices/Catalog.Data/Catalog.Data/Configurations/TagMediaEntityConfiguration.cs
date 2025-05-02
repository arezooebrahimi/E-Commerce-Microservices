using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Data.Configurations
{
    public class TagMediaEntityConfiguration : IEntityTypeConfiguration<TagMedia>
    {
        public void Configure(EntityTypeBuilder<TagMedia> builder)
        {

            builder.HasKey(pc => new { pc.MediaId, pc.TagId });

            builder.HasOne(pc => pc.Tag)
                   .WithMany(p => p.Medias)
                   .HasForeignKey(pc => pc.TagId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
