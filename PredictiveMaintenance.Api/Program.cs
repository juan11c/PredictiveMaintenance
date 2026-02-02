using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Application.Interfaces;
using PredictiveMaintenance.Application.Services;
using PredictiveMaintenance.Infrastructure.Persistence;
using PredictiveMaintenance.Infrastructure.Services;
using System;
using System.Reflection;
using System.IO;



var builder = WebApplication.CreateBuilder(args);

// Configuración de EF Core con SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddScoped<ITelemetryService, TelemetryService>();

// Add controllers
builder.Services.AddControllers();
builder.Services.AddScoped<IMachineService, MachineService>();



// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "PredictiveMaintenance API",
        Version = "v1",
        Description = "API para gestión de máquinas y mantenimiento predictivo",
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    var xmlApi = Path.Combine(AppContext.BaseDirectory, "PredictiveMaintenance.Api.xml");
    c.IncludeXmlComments(xmlApi);

    var xmlApplication = Path.Combine(AppContext.BaseDirectory, "PredictiveMaintenance.Application.xml");
    c.IncludeXmlComments(xmlApplication);
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PredictiveMaintenance API v1");
        c.RoutePrefix = string.Empty; // Swagger en la raíz (http://localhost:5139/)
    });
}

app.UseHttpsRedirection();
app.MapControllers(); // Add MapControllers
app.UseMiddleware<PredictiveMaintenance.Api.Middlewares.ErrorHandlingMiddleware>();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
