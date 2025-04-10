using Fiap.Infra.CrossCutting.IoC;
using Fiap.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddLocalHttpClients(builder.Configuration);
builder.Services.AddLocalServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap["lowercase"] = typeof(string);
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FIAP API",
        Version = "v1",
        Description = "FIAP API to evaluate our knowledge"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FIAP API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
