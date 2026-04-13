using MetricDb;
using Microsoft.AspNetCore.Rewrite;
using PanicDb;
using Scalar.AspNetCore;
using Serilog;

Serilog.Debugging.SelfLog.Enable(Console.Error);
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    // TODO: Add Transformer
});

builder.Services.AddPanicDb();
builder.Services.AddMetricDb();

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Starting application in {Environment} environment", app.Environment.EnvironmentName);

var rewriteOptions = new RewriteOptions();
rewriteOptions.AddRedirect("^$", "scalar/v1");
app.UseRewriter(rewriteOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.Title = $"Panic {builder.Environment.EnvironmentName}";
});

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();

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
