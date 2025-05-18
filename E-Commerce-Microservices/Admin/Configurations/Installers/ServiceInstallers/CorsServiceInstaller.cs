using Common.Attributes;
using Common.Filters;

namespace Admin.Configurations.Installers.ServiceInstallers
{
    [InstallerOrder(Order = 4)]
    public class CorsServiceInstaller : IServiceInstaller
    {
        public Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecific", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:5173")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            return Task.CompletedTask;
        }
    }

}
