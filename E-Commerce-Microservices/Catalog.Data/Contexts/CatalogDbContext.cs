using Catalog.Data.Configurations;
using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Data.Contexts
{
    public class CatalogDbContext:DbContext
    {
        private readonly IConfiguration _configuration;
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<FeatureOption> FeatureOptions { get; set; }
        public DbSet<ProductVariable> ProductVariables { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductMedia> ProductMedias { get; set; }
        public DbSet<CategoryMedia> CategoryMedias { get; set; }
        public DbSet<TagMedia> TagMedias { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTagEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FeatureEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FeatureOptionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductVariableEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductRelatedEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryMediaEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductMediaEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TagMediaEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductFeatureEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductReviewEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductQuestionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TagEntityConfiguration());
        }
    }
}
