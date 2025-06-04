using Common.Attributes;
using System.Reflection;

namespace Basket.API.Configurations.Installers;

public static class DependencyInjection
{
    public async static Task<IServiceCollection> InstallServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment hostEnvironment,
        params Assembly[] assemblies)
    {
        IEnumerable<IServiceInstaller> serviceInstallers = assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(IsAssignableToType<IServiceInstaller>)
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>()
            .OrderBy(ord =>
            {
                var att = ord.GetType()
                             .GetCustomAttributes(typeof(InstallerOrderAttribute), true)
                             .FirstOrDefault() as InstallerOrderAttribute;

                if (att == null)
                    return int.MaxValue;

                return att.Order;
            });

        foreach (IServiceInstaller serviceInstaller in serviceInstallers)
        {
            await serviceInstaller.Install(services, configuration, hostEnvironment);
        }

        return services;

        static bool IsAssignableToType<T>(TypeInfo typeInfo) =>
            typeof(T).IsAssignableFrom(typeInfo) &&
            !typeInfo.IsInterface &&
            !typeInfo.IsAbstract;
    }

    public static WebApplication InstallWebApp(
    this WebApplication app,
    IHostApplicationLifetime appLifeTime,
    IConfiguration configuration,
    params Assembly[] assemblies)
    {
        IEnumerable<IWebApplicationInstaller> webAppInstallers = assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(IsAssignableToType<IWebApplicationInstaller>)
            .Select(Activator.CreateInstance)
            .Cast<IWebApplicationInstaller>();

        foreach (IWebApplicationInstaller webAppIstaller in webAppInstallers)
        {
            webAppIstaller.Install(app, appLifeTime, configuration);
        }

        return app;

        static bool IsAssignableToType<T>(TypeInfo typeInfo) =>
            typeof(T).IsAssignableFrom(typeInfo) &&
            !typeInfo.IsInterface &&
            !typeInfo.IsAbstract;
    }

}
