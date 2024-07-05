using Asp.Versioning;
using Catalog.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = new ApiVersion(1, 0);
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.ReportApiVersions = true;
}).AddMvc();


var app = builder.Build();


app.UseCustomExceptionHandler();
app.MapControllers();
app.Run();
