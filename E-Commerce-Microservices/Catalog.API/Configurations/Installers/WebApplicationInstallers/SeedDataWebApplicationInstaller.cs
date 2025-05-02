using Catalog.Data.Contexts;
using Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Catalog.API.Configurations.Installers.WebApplicationInstallers
{
    public class SeedDataWebApplicationInstaller : IWebApplicationInstaller
    {
        public async void Install(WebApplication app, IHostApplicationLifetime lifeTime, IConfiguration configuration)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedDataWebApplicationInstaller>>();

            try
            {
                await SeedIfEmpty<Category>(context.Categories, "categories.json", context, logger);
                await SeedIfEmpty<Brand>(context.Brands, "brands.json", context, logger);
                await SeedIfEmpty<Feature>(context.Features, "product-features.json", context, logger);
                await SeedIfEmpty<FeatureOption>(context.FeatureOptions, "product-feature-options.json", context, logger);
                await SeedIfEmpty<Tag>(context.Tags, "tags.json", context, logger);
                await SeedIfEmpty<Product>(context.Products, "products.json", context, logger);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }

        }

        private static async Task SeedIfEmpty<T>(
        DbSet<T> dbSet,
        string fileName,
        CatalogDbContext context,
        ILogger logger) where T : class
        {
            if (await dbSet.AnyAsync()) return;

            var basePath = "../Catalog.Data/SeedData";
            var filePath = Path.Combine(basePath, fileName);
            
            if (!File.Exists(filePath))
            {
                logger.LogWarning($"Seed file not found: {filePath}");
                return;
            }

            string json = await File.ReadAllTextAsync(filePath);
            var data = JsonSerializer.Deserialize<List<T>>(json,new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (data != null)
            {
                await dbSet.AddRangeAsync(data);
                await context.SaveChangesAsync();
            }
        }
    }
} 