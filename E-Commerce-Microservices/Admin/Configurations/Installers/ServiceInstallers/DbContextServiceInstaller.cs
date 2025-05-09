using Admin.Configurations.Installers;
using Common.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Configurations.Installers.ServiceInstallers;

public class DbContextServiceInstaller: IServiceInstaller
{
    public Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        string defaultConnString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<CatalogDbContext>(options => options.UseNpgsql(defaultConnString));


        return Task.CompletedTask;
    }
}

