using Auth.API.Configurations.Installers;
using Auth.API.Middlewares;
using Auth.Services.Grpc;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

await builder.Services.InstallServices(
    configuration,
    environment,
    typeof(IServiceInstaller).Assembly
);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddGrpc();
var app = builder.Build();

app.InstallWebApp(
    app.Lifetime,
    configuration,
    typeof(IWebApplicationInstaller).Assembly
);

// Add custom exception handler
app.UseCustomExceptionHandler();


app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<GrpcAuthService>();
app.MapGet("/", () => "Auth gRPC is running!");
app.UseCors("AllowSpecific");
app.MapControllers();
app.Run();