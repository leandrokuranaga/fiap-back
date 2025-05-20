using Swashbuckle.AspNetCore.Filters;
using Fiap.Application.Common;
using Fiap.Domain.SeedWork;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class GenericErrorBadRequestExample : IExamplesProvider<BaseResponse<object>>
{
    public BaseResponse<object> GetExamples()
    {
        var notification = new NotificationModel
        {
            NotificationType = NotificationModel.ENotificationType.BadRequestError
        };

        notification.AddMessage("Field", "Field Required");

        notification.AddMessage("Error", "Generic validation error");

        return BaseResponse<object>.Fail(notification);
    }
}
