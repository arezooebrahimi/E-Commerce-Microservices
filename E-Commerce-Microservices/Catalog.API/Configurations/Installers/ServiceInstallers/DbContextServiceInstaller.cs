using Catalog.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Configurations.Installers.ServiceInstallers;

public class DbContextServiceInstaller: IServiceInstaller
{
    public Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        var assembly = typeof(CatalogDbContext).Assembly.GetName().Name;

        string defaultConnString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


        services.AddDbContext<CatalogDbContext>(options =>
        {
            options.UseNpgsql(connectionString: defaultConnString,
                                 npgsqlOptionsAction: sqlOptions =>
                                 {
                                     sqlOptions.MigrationsAssembly(assembly);
                                     sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30),errorCodesToAdd:null);
                                 });
        }, ServiceLifetime.Scoped);

        var serviceProvider = services.BuildServiceProvider();
        var context = serviceProvider.GetRequiredService<CatalogDbContext>();


        context.Database.Migrate();

        return Task.CompletedTask;
    }
}

