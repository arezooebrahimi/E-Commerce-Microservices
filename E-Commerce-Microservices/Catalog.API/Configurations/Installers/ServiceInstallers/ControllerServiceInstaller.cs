using Asp.Versioning;
using Common.Attributes;

namespace Catalog.API.Configurations.Installers.ServiceInstallers;

[InstallerOrder(Order = 1)]
public class ControllerServiceInstaller : IServiceInstaller
{
    public Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.ReportApiVersions = true;
        }).AddMvc().AddApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        return Task.CompletedTask;
    }
}
