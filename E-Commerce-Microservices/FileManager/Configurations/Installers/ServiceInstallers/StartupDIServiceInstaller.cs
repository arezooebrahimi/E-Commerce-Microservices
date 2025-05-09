
using Common.Attributes;
using FileManager.Services.Abstract;
using FileManager.Services.Concrete;

namespace FileManager.Configurations.Installers.ServiceInstallers;

[InstallerOrder(Order = 1)]
public class StartupDIServiceInstaller : IServiceInstaller
{
    public Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IArvanFileService, ArvanFileService>();
        return Task.CompletedTask;
    }
}
