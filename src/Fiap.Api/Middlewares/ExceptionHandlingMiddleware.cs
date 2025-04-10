// Fiap.Api/Middlewares/ExceptionHandlingMiddleware.cs
using System.Net;
using System.Text.Json;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                message = ex.Message,
                stackTrace = ex.StackTrace
            };

            var json = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(json);
        }
    }
}
