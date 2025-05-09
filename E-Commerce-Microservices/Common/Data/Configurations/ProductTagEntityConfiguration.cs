using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Data.Configurations
{
    public class ProductTagEntityConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {

            builder.HasKey(pc => new { pc.ProductId, pc.TagId });

            builder.HasOne(pc => pc.Product)
                   .WithMany(p => p.Tags)
                   .HasForeignKey(pc => pc.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pc => pc.Tag)
                   .WithMany(c => c.Products)
                   .HasForeignKey(pc => pc.TagId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 