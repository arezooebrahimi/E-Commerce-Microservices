using Auth.Data.Contexts;
using Common.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Configurations.Installers.ServiceInstallers;


[InstallerOrder(Order = 3)]
public class DbContextServiceInstaller: IServiceInstaller
{
    public Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {

        string defaultConnString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


        services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseNpgsql(connectionString: defaultConnString,
                                 npgsqlOptionsAction: sqlOptions =>
                                 {
                                     sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30),errorCodesToAdd:null);
                                 });
        }, ServiceLifetime.Scoped);

        var serviceProvider = services.BuildServiceProvider();
        var context = serviceProvider.GetRequiredService<AuthDbContext>();


        context.Database.Migrate();

        return Task.CompletedTask;
    }
}

