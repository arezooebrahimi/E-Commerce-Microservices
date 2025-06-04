using Basket.Services.Abstract;
using Basket.Services.Concrete;
using Basket.Services.Grpc;
using Common.Attributes;

namespace Basket.API.Configurations.Installers.ServiceInstallers;

[InstallerOrder(Order = 1)]
public class StartupDIServiceInstaller : IServiceInstaller
{
    public Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        services.AddScoped<IBasketService, BasketService>();
        services.AddSingleton<GrpcAuthClient>();

        return Task.CompletedTask;
    }
}
