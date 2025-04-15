using Fiap.Api.Extensions;
using Fiap.Infra.CrossCutting.IoC;
using Fiap.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Fiap.Domain.GameAggregate; 
using Fiap.Infra.Data.Repositories; 


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

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

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCustomStatusCodePages();

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
