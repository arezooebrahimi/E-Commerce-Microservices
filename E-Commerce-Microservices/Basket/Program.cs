using Basket.API.Configurations.Installers;
using Basket.Endpoints.v1;
using Basket.Extensions;
using Basket.Models;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;


await builder.Services.InstallServices(
    configuration,
    environment,
    typeof(IServiceInstaller).Assembly
);

builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<RemoveBasketItemRequestValidator>();


var app = builder.Build();

app.InstallWebApp(
    app.Lifetime,
    configuration,
    typeof(IWebApplicationInstaller).Assembly
);

app.UseAuthMiddleware();
app.MapV1BasketEndpoints();
app.Run();