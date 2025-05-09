
using Admin.Configurations.Installers;
using Admin.Repositories.Abstract;
using Admin.Repositories.Concrete;
using Admin.Services.Abstract;
using Admin.Services.Concrete;
using Common.Attributes;


namespace FileManager.Configurations.Installers.ServiceInstallers;

[InstallerOrder(Order = 1)]
public class StartupDIServiceInstaller : IServiceInstaller
{
    public Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return Task.CompletedTask;
    }
}
