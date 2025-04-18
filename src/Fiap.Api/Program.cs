using Fiap.Api.Extensions;
using Fiap.Infra.CrossCutting.IoC;
using Fiap.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Extensions.Hosting;
using Serilog.Formatting.Json;
using Serilog.Sinks;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddLocalHttpClients(builder.Configuration);
builder.Services.AddLocalServices(builder.Configuration);

builder.Services.AddCustomMvc();

builder.Services.AddHealthChecks()
    .AddNpgSql(connectionString);    

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddGlobalCorsPolicy();

builder.Services.AddApiVersioningConfiguration();

builder.Services.AddSwaggerDocumentation();

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .MinimumLevel.Error()
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", "Fiap.Api")
        .WriteTo.Console(new JsonFormatter(renderMessage: true))
        .WriteTo.File(new JsonFormatter(renderMessage: true), "logs/log-.json", rollingInterval: RollingInterval.Day);
    ;
});

var app = builder.Build();

app.UseExceptionHandling();

//app.UseCustomStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");


app.Run();
