using Common.Attributes;
using Microsoft.OpenApi.Models;

namespace FileManager.Configurations.Installers.ServiceInstallers;


[InstallerOrder(Order = 2)]
public class ApiDocumentationServiceInstaller : IServiceInstaller
{
    public Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        services.AddSwaggerGen();

        return Task.CompletedTask;
    }
}
