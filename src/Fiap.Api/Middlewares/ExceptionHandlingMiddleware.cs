using Fiap.Domain.SeedWork.Exceptions;
using Fiap.Domain.SeedWork;
using Serilog;
using System.Net;
using System.Text.Json;
using Fiap.Application.Common;

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
            await RollbackIfPossibleAsync(context);
            await HandleExceptionAsync(context, e, "Not Found", NotificationModel.ENotificationType.NotFound, HttpStatusCode.NotFound);
        }
        catch (ArgumentException e)
        {
            await RollbackIfPossibleAsync(context);
            await HandleExceptionAsync(context, e, "Invalid Property", NotificationModel.ENotificationType.BadRequestError, HttpStatusCode.BadRequest);
        }
        catch (BusinessRulesException e)
        {
            await RollbackIfPossibleAsync(context);
            await HandleExceptionAsync(context, e, "Business Rules", NotificationModel.ENotificationType.BusinessRules, HttpStatusCode.UnprocessableEntity);
        }
        catch (Exception e)
        {
            await RollbackIfPossibleAsync(context);
            await HandleExceptionAsync(context, e, "Internal Error", NotificationModel.ENotificationType.InternalServerError, HttpStatusCode.InternalServerError);
        }
    }

    private async Task RollbackIfPossibleAsync(HttpContext context)
    {
        var uow = context.RequestServices.GetService<IUnitOfWork>();
        if (uow != null)
        {
            try
            {
                await uow.RollbackAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao tentar fazer rollback da transação");
            }
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, string title, NotificationModel.ENotificationType type, HttpStatusCode statusCode)
    {
        var correlationId = Guid.NewGuid().ToString();
        var userAgent = context.Request.Headers.UserAgent.ToString();

        Log.Error(exception,
            "Unhandled exception occurred. CorrelationId: {CorrelationId}, Path: {Path}, Method: {Method}, QueryString: {QueryString}, UserAgent: {UserAgent}",
            correlationId,
            context.Request.Path.Value,
            context.Request.Method,
            context.Request.QueryString.Value,
            userAgent);

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var isDev = context.RequestServices.GetService<IWebHostEnvironment>()?.IsDevelopment() == true;
        var message = isDev ? exception.Message : "An unexpected error occurred.";

        var notification = new NotificationModel
        {
            NotificationType = type
        };
        notification.AddMessage(title, message);

        var response = BaseResponse<object>.Fail(notification);

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }


}
