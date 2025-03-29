using Fiap.Application;
using Fiap.Application.Common;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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

        protected new IActionResult Response(BaseResponse response)
        {
            if (IsValidOperation())
            {
                if (response == null)
                    return NoContent();

                return Ok(response);
            }
            else
            {
                if (response == null)
                    response = new Response();

                response.Success = false;
                response.Error = _notification.NotificationModel;
                switch (_notification.NotificationModel.NotificationType)
                {
                    case ENotificationType.BusinessRules:
                        return Conflict(response);
                    case ENotificationType.NotFound:
                        return NotFound(response);
                    case ENotificationType.BadRequestError:
                        return BadRequest(response);
                    default:
                        return StatusCode((int)HttpStatusCode.InternalServerError, response);
                }
            }
        }

        protected new IActionResult Response(object response)
        {
            if (IsValidOperation())
            {
                if (response == null)
                    return NoContent();

                return Ok(new
                {
                    success = true,
                    data = response
                });
            }

            return BadRequest(new
            {
                success = false,
                error = _notification.NotificationModel
            });
        }

        protected new IActionResult Response(int? id, object response)
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

    }
}
