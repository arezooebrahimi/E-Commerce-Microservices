using Basket.API.Configurations.Installers;
using Basket.Endpoints.v1;
using Basket.Extensions;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;


await builder.Services.InstallServices(
    configuration,
    environment,
    typeof(IServiceInstaller).Assembly
);

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.InstallWebApp(
    app.Lifetime,
    configuration,
    typeof(IWebApplicationInstaller).Assembly
);

app.UseAuthMiddleware();
app.MapV1BasketEndpoints();
app.Run();