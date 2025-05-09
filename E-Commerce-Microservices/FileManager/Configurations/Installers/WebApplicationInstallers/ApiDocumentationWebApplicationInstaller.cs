using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace FileManager.Configurations.Installers.WebApplicationInstallers;

public class ApiDocumentationWebApplicationInstaller : IWebApplicationInstaller
{
    public void Install(WebApplication app, IHostApplicationLifetime lifeTime, IConfiguration configuration)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
