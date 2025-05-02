using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Data.Configurations
{
    public class FeatureOptionEntityConfiguration : IEntityTypeConfiguration<FeatureOption>
    {
        public void Configure(EntityTypeBuilder<FeatureOption> builder)
        {

            builder.HasKey(pao => pao.Id);
            builder.Property(pao => pao.Id).ValueGeneratedOnAdd();

            builder.Property(pao => pao.Name).IsRequired().HasMaxLength(300);
            builder.Property(pao => pao.Slug).IsRequired().HasMaxLength(300);
            builder.HasIndex(p => p.Slug).IsUnique();
            builder.Property(pao => pao.Description).HasColumnType("text");

            builder.Property(pao => pao.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(pao => pao.Feature)
                   .WithMany(pa => pa.Options)
                   .HasForeignKey(pao => pao.FeatureId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 