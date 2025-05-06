using Fiap.Api.Middlewares;

public static class ErrorHandlingExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}