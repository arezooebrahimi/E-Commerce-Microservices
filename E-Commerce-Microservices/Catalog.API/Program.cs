using Catalog.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();


app.UseCustomExceptionHandler();
app.MapControllers();
app.Run();
