using FileManager.Configurations.Installers;
using FileManager.Middlewares;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

// Register services
await builder.Services.InstallServices(
    configuration,
    environment,
    typeof(IServiceInstaller).Assembly
);
var app = builder.Build();
app.InstallWebApp(
    app.Lifetime,
    configuration,
    typeof(IWebApplicationInstaller).Assembly
);
app.UseCustomExceptionHandler();
app.MapControllers();
app.Run();
