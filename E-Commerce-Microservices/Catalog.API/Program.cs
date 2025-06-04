using Catalog.API.Configurations.Installers;
using Catalog.API.Middlewares;


var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

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
app.UseCors("AllowSpecific");
app.MapControllers();
app.Run();
