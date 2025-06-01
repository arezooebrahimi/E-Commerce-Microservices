
using Admin.Configurations.Installers;
using Admin.Repositories.Abstract;
using Admin.Repositories.Concrete;
using Admin.Services.Abstract;
using Admin.Services.Concrete;
using Admin.Services.Grpc;
using Common.Attributes;


namespace FileManager.Configurations.Installers.ServiceInstallers;

[InstallerOrder(Order = 3)]
public class StartupDIServiceInstaller : IServiceInstaller
{
    public Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IFeatureService, FeatureService>();
        services.AddScoped<IFeatureOptionService, FeatureOptionService>();
        services.AddScoped<ICategoryMediaRepository, CategoryMediaRepository>();
        services.AddSingleton<FileManagerGrpcClient>();
        return Task.CompletedTask;
    }
}
