using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Data.Configurations
{
    public class FeatureEntityConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.HasKey(pa => pa.Id);
            builder.Property(pa => pa.Id).ValueGeneratedOnAdd();

            builder.Property(pa => pa.Name).IsRequired().HasMaxLength(300);
            builder.Property(pa => pa.Slug).IsRequired().HasMaxLength(300);
            builder.Property(pa => pa.Type).IsRequired().HasMaxLength(50);

            builder.Property(pa => pa.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasMany(pa => pa.Options)
                   .WithOne(pao => pao.Feature)
                   .HasForeignKey(pao => pao.FeatureId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 