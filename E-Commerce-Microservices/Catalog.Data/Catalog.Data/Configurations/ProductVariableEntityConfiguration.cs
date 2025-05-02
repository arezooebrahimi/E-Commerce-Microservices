using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Data.Configurations
{
    public class ProductVariableEntityConfiguration : IEntityTypeConfiguration<ProductVariable>
    {
        public void Configure(EntityTypeBuilder<ProductVariable> builder)
        {
            builder.ToTable("ProductVariables");

            builder.HasKey(pv => pv.Id);
            builder.Property(pv => pv.Id).ValueGeneratedOnAdd();

            builder.Property(pv => pv.ProductNumber).HasMaxLength(100);
            builder.Property(pv => pv.Description).HasColumnType("text");

            builder.Property(pv => pv.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(pv => pv.Product)
                   .WithMany(p => p.Variables)
                   .HasForeignKey(pv => pv.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(pv => pv.FeatureOption)
                   .WithMany(p => p.Variables)
                   .HasForeignKey(pv => pv.FeatureOptionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 