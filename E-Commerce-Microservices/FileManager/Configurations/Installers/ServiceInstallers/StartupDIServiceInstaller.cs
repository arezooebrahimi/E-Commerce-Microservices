
using Common.Attributes;
using FileManager.Models.Mongodb;
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

        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDB"));
        services.AddScoped<IMediaService, MediaService>();
        services.AddScoped<IImageProcessingService, ImageProcessingService>();

        return Task.CompletedTask;
    }
}
