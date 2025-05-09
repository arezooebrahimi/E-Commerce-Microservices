using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Data.Configurations
{
    public class ProductFeatureEntityConfiguration : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {

            builder.HasKey(pc => new { pc.ProductId, pc.FeatureId });

            builder.HasOne(pc => pc.Product)
                   .WithMany(p => p.Features)
                   .HasForeignKey(pc => pc.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pc => pc.Feature)
                   .WithMany(c => c.Products)
                   .HasForeignKey(pc => pc.FeatureId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pc => pc.DefaultFeature)
                  .WithMany(c => c.DefaultProductOptions)
                  .HasForeignKey(pc => pc.DefaultFeatureOptionId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
