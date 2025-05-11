using Admin.Configurations.Installers;
using Common.Attributes;
using Common.Filters;

namespace FileManager.Configurations.Installers.ServiceInstallers;

[InstallerOrder(Order = 1)]
public class ControllerServiceInstaller : IServiceInstaller
{
    public Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ApiValidationFilter>();
        });
        //services.AddEndpointsApiExplorer();

        return Task.CompletedTask;
    }
}
