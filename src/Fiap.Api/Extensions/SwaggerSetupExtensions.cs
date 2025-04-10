using Fiap.Api.SwaggerExamples.Promotions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Fiap.Api.Extensions
{
    public static class SwaggerSetupExtensions
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FIAP API",
                    Version = "v1",
                    Description = "FIAP API to evaluate our knowledge"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                c.ExampleFilters();
                c.OperationFilter<SetApplicationJsonAsDefaultFilter>();
            });

            services.AddSwaggerExamplesFromAssemblyOf<CreatePromotionRequestExample>();
        }

        public static void UseSwaggerDocumentation(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FIAP API v1");
                c.RoutePrefix = "swagger";
            });
        }
    }

}
