using System.Net;
using System.Text.Json;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var correlationId = Guid.NewGuid().ToString();

            // Log estruturado com detalhes do contexto
            _logger.LogError(ex, "Unhandled exception occurred. CorrelationId: {CorrelationId}, Path: {Path}, Method: {Method}, QueryString: {QueryString}",
               correlationId,
               context.Request.Path,
               context.Request.Method,
               context.Request.QueryString);

            // Configurar a resposta de erro
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