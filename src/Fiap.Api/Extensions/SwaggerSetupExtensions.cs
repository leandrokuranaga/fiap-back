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

                c.ExampleFilters();
            });

            services.AddSwaggerExamplesFromAssemblyOf<CreatePromotionRequestExample>();
        }

        public static void UseSwaggerDocumentation(this IApplicationBuilder app)
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
