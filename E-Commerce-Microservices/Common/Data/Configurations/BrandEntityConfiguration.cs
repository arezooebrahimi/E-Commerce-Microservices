using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.Data.Configurations
{
    public class BrandEntityConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasMany(c => c.Products)
                   .WithOne(c => c.Brand)
                   .HasForeignKey(c => c.BrandId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
