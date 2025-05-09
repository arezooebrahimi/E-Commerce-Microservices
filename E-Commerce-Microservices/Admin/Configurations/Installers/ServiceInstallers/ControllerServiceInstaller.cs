using Admin.Configurations.Installers;
using Common.Attributes;

namespace FileManager.Configurations.Installers.ServiceInstallers;

[InstallerOrder(Order = 1)]
public class ControllerServiceInstaller : IServiceInstaller
{
    public Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        services.AddControllers();
        //services.AddEndpointsApiExplorer();

        return Task.CompletedTask;
    }
}
