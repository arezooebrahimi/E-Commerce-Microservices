using Catalog.Data.Repositories.Dapper.Abstract;
using Catalog.Data.Repositories.Dapper.Concrete;
using Catalog.Data.Repositories.EntityFramework.Abstract;
using Catalog.Data.Repositories.EntityFramework.Concrete;
using Catalog.Service.v1.Abstract;
using Catalog.Service.v1.Concrete;
using Common.Attributes;

namespace Catalog.API.Configurations.Installers.ServiceInstallers;

[InstallerOrder(Order = 1)]
public class StartupDIServiceInstaller : IServiceInstaller
{
    public Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();


        services.AddScoped<Service.v2.Abstract.ICategoryService, Service.v2.Concrete.CategoryService>();
        services.AddScoped<ICategoryDapperRepository, CategoryDapperRepository>();

        return Task.CompletedTask;
    }
}
