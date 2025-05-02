using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Data.Configurations
{
    public class ProductRelatedEntityConfiguration : IEntityTypeConfiguration<ProductRelated>
    {
        public void Configure(EntityTypeBuilder<ProductRelated> builder)
        {
            builder.HasKey(pc => new { pc.ProductId, pc.RelatedProductId });

            builder.HasOne(pc => pc.Product)
                   .WithMany(p => p.LinkedProducts)
                   .HasForeignKey(pc => pc.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pc => pc.RelatedProduct)
                   .WithMany(c => c.RelatedProducts)
                   .HasForeignKey(pc => pc.RelatedProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
