using Fiap.Application.Common;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using static Fiap.Domain.SeedWork.NotificationModel;

namespace Fiap.Api
{
    public class BaseController : ControllerBase
    {
        private readonly INotification _notification;

        protected BaseController(INotification notification)
        {
            _notification = notification;
        }

        private bool IsValidOperation() => !_notification.HasNotification;

        protected IActionResult Response<T>(BaseResponse<T> response)
        {
            if (IsValidOperation())
            {
                if (response.Data == null)
                    return NoContent();

                return Ok(response);
            }

            response.Success = false;
            response.Data = default; 
            response.Error = _notification.NotificationModel;

            return response.Error.NotificationType switch
            {
                ENotificationType.BusinessRules => Conflict(response),
                ENotificationType.NotFound => NotFound(response),
                ENotificationType.BadRequestError => BadRequest(response),
                _ => StatusCode((int)HttpStatusCode.InternalServerError, response)
            };
        }

        protected new IActionResult Response<T>(int? id, object response)
        {
            if (IsValidOperation())
            {
                if (id == null)
                    return Ok(new
                    {
                        success = true,
                        data = response
                    });

                return CreatedAtAction("Get", new { id },
                    new
                    {
                        success = true,
                        data = response ?? new object()
                    });
            }

            return BadRequest(new
            {
                success = false,
                error = _notification.NotificationModel
            });
        }

        protected string GetLoggedUser()
        {
            var userIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var user = userIdentity?.Claims.Where(c => c.Type == "Id").FirstOrDefault();
            return user == null ? string.Empty : user.Value;
        }
    }
}
