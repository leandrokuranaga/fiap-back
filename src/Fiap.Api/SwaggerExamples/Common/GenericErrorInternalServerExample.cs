using Swashbuckle.AspNetCore.Filters;
using Fiap.Application.Common;
using Fiap.Domain.SeedWork;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class GenericErrorInternalServerExample : IExamplesProvider<BaseResponse<object>>
{
    public BaseResponse<object> GetExamples()
    {
        var notification = new NotificationModel
        {
            NotificationType = NotificationModel.ENotificationType.InternalServerError
        };

        notification.AddMessage("Server", "An unexpected error occurred while processing your request.");
        notification.AddMessage("Internal", "Please contact support if the problem persists.");

        return BaseResponse<object>.Fail(notification);
    }
}
