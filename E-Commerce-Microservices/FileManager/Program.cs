using FileManager.Configurations.Installers;
using FileManager.Middlewares;
using FileManager.Services.Grpc;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

// Register services
await builder.Services.InstallServices(
    configuration,
    environment,
    typeof(IServiceInstaller).Assembly
);
builder.Services.AddGrpc();

var app = builder.Build();
app.InstallWebApp(
    app.Lifetime,
    configuration,
    typeof(IWebApplicationInstaller).Assembly
);
app.UseCustomExceptionHandler();
app.MapGrpcService<FileManagerGrpcServer>();
app.MapGet("/", () => "FileManager gRPC is running!");
app.MapControllers();
app.Run();
