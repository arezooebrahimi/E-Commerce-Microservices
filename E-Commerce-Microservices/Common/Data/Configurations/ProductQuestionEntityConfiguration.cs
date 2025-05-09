using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Data.Configurations
{
    public class ProductQuestionEntityConfiguration : IEntityTypeConfiguration<ProductQuestion>
    {
        public void Configure(EntityTypeBuilder<ProductQuestion> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
} 