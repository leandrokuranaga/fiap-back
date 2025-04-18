using Serilog;
using System.Net;
using System.Text.Json;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var correlationId = Guid.NewGuid().ToString();

            var userAgent = context.Request.Headers["User-Agent"].ToString();
            Log.Error(ex,
                "Unhandled exception occurred. CorrelationId: {CorrelationId}, Path: {Path}, Method: {Method}, QueryString: {QueryString}, UserAgent: {UserAgent}",
                correlationId,
                context.Request.Path.Value,
                context.Request.Method,
                context.Request.QueryString.Value,
                userAgent);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                message = "An unexpected error occurred.",
                details = context.RequestServices.GetService<IWebHostEnvironment>()?.IsDevelopment() == true
                    ? ex.Message
                    : null,
                path = context.Request.Path.Value,
                method = context.Request.Method,
                correlationId = correlationId,
                timestamp = DateTime.UtcNow
            };

            var json = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(json);
        }
    }
}