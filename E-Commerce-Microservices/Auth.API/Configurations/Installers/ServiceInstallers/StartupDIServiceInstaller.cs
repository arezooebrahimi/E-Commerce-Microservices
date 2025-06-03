using Auth.Models;
using Common.Attributes;


namespace Auth.API.Configurations.Installers.ServiceInstallers;

[InstallerOrder(Order = 1)]
public class StartupDIServiceInstaller : IServiceInstaller
{
    public Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        return Task.CompletedTask;
    }
}
