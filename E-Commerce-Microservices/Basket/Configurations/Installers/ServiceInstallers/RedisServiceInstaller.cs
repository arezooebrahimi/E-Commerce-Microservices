using Basket.API.Configurations.Installers;
using Common.Attributes;
using StackExchange.Redis;

namespace Basket.Configurations.Installers.ServiceInstallers
{
    [InstallerOrder(Order = 3)]
    public class RedisServiceInstaller : IServiceInstaller
    {
        public Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis") ?? "localhost:6379"));
            return Task.CompletedTask;
        }
    }
}
