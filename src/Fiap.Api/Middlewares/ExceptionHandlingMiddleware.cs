using Fiap.Domain.SeedWork.Exceptions;
using Fiap.Domain.SeedWork;
using Serilog;
using System.Net;
using System.Text.Json;

namespace Fiap.Api.Middlewares
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException e)
            {     
                await HandleExceptionAsync(context, e, "Not Found", NotificationModel.ENotificationType.NotFound, HttpStatusCode.NotFound);
            }
            catch (ArgumentException e)
            {             
                await HandleExceptionAsync(context, e, "Invalid Property", NotificationModel.ENotificationType.BadRequestError, HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {                
                await HandleExceptionAsync(context, e, "Internal Error", NotificationModel.ENotificationType.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, string title, NotificationModel.ENotificationType notificationType, HttpStatusCode statusCode)
        {
            var correlationId = Guid.NewGuid().ToString();
            var userAgent = context.Request.Headers["User-Agent"].ToString();

            Log.Error(exception,
                "Unhandled exception occurred. CorrelationId: {CorrelationId}, Path: {Path}, Method: {Method}, QueryString: {QueryString}, UserAgent: {UserAgent}",
                correlationId,
                context.Request.Path.Value,
                context.Request.Method,
                context.Request.QueryString.Value,
                userAgent);

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                message = title,
                details = context.RequestServices.GetService<IWebHostEnvironment>()?.IsDevelopment() == true
                    ? exception.Message
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