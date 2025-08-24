using backend.Extensions;
using backend.Middleware;

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DGII Tax Management API",
        Version = "v1",
        Description = "API para el Sistema de Gestión Tributaria - DGII",
        Contact = new OpenApiContact
        {
            Name = "DGII",
            Email = "contacto@dgii.gov.do"
        }
    });
});

builder.Services.AddApplicationServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("*")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DGII Tax Management API v1");
    c.RoutePrefix = "swagger";
    c.DocumentTitle = "DGII Tax Management API";
});

app.UseMiddleware<ErrorHandlingMiddleware>();


app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();

app.Logger.LogInformation("🚀 DGII API iniciada correctamente");
app.Logger.LogInformation("📖 Swagger UI: http://localhost:5000/swagger");
app.Logger.LogInformation("🔗 API Base: http://localhost:5000/api");

app.Run();