using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Data.Configurations
{
    public class ProductMediaEntityConfiguration : IEntityTypeConfiguration<ProductMedia>
    {
        public void Configure(EntityTypeBuilder<ProductMedia> builder)
        {

            builder.HasKey(pc => new { pc.MediaId, pc.ProductId });

            builder.HasOne(pc => pc.Product)
                   .WithMany(p => p.Medias)
                   .HasForeignKey(pc => pc.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
