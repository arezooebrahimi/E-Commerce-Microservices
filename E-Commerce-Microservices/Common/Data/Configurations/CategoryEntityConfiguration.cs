using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Data.Configurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Name).IsRequired().HasMaxLength(300);
            builder.Property(c => c.Slug).IsRequired().HasMaxLength(300);
            builder.HasIndex(p => p.Slug).IsUnique();
            builder.Property(c => c.Description).HasColumnType("text");

            builder.Property(c => c.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(c => c.Parent)
                   .WithMany(c => c.SubCategories)
                   .HasForeignKey(c => c.ParentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Medias)
                   .WithOne(cm => cm.Category)
                   .HasForeignKey(cm => cm.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
