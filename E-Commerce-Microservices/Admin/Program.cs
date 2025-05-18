using Admin.Configurations.Installers;
using Admin.Middlewares;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;


// Register services
await builder.Services.InstallServices(
    configuration,
    environment,
    typeof(IServiceInstaller).Assembly
);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});



var app = builder.Build();
app.InstallWebApp(
    app.Lifetime,
    configuration,
    typeof(IWebApplicationInstaller).Assembly
);
app.UseCors("AllowSpecific");
app.UseCustomExceptionHandler();
app.MapControllers();


app.Run();
