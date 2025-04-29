using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Fiap.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ErrorHandlingExtensions
    {
        public static void UseCustomStatusCodePages(this IApplicationBuilder app)
        {
            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;

                response.ContentType = "application/json";
                var message = response.StatusCode switch
                {
                    StatusCodes.Status404NotFound => new { message = "Endpoint not found or invalid parameter", status = 404 },
                    StatusCodes.Status405MethodNotAllowed => new { message = "HTTP Method not allowed for this endpoint", status = 405 },
                    StatusCodes.Status406NotAcceptable => new { message = "Response type not supported. Use 'Content-Type: application/json' and remove the Accept header or 'Accept: */*'.", status = 406 },
                    _ => null
                };

                if (message != null)
                {
                    await response.WriteAsync(JsonSerializer.Serialize(message));
                }
            });
        }
    }

}
