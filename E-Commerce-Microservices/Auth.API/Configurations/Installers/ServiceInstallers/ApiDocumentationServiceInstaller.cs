using Common.Attributes;
using Microsoft.OpenApi.Models;

namespace Auth.API.Configurations.Installers.ServiceInstallers;


[InstallerOrder(Order = 2)]
public class ApiDocumentationServiceInstaller : IServiceInstaller
{
    public Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v2", new OpenApiInfo { Title = "E-Commerce API V2", Version = "v2" });
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "E-Commerce API v1",
                Description = "Catalog Service API",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Example Contact",
                    Url = new Uri("https://example.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://example.com/license")
                }
            });
        });

        return Task.CompletedTask;
    }
}
