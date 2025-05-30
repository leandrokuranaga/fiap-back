using Fiap.Api.Extensions;
using Fiap.Api.Middlewares;
using Fiap.Application.Common;
using Fiap.Domain.SeedWork;
using Fiap.Infra.CrossCutting.IoC;
using Fiap.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

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

builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Services.AddSwaggerDocumentation();

builder.Host.UseSerilog((context, services, configuration) =>
{
    SerilogExtensions.ConfigureSerilog(context, services, configuration);
});

var app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseRouting();


app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseMiddleware<UnauthorizedResponseMiddleware>();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");


app.Run();

[ExcludeFromCodeCoverage]
public partial class Program { }