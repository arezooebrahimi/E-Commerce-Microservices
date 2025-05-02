using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Data.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name).IsRequired().HasMaxLength(300);
            builder.Property(p => p.Slug).IsRequired().HasMaxLength(300);
            builder.HasIndex(p => p.Slug).IsUnique();
            builder.Property(p => p.ShortDescription).HasMaxLength(500);
            builder.Property(p => p.Description).HasColumnType("text");
            builder.Property(p => p.Tag).HasMaxLength(100);

            builder.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");


            builder.HasMany(p => p.Medias)
                   .WithOne(pm => pm.Product)
                   .HasForeignKey(pm => pm.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Variables)
                   .WithOne(pv => pv.Product)
                   .HasForeignKey(pv => pv.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Reviews)
                   .WithOne(pr => pr.Product)
                   .HasForeignKey(pr => pr.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 