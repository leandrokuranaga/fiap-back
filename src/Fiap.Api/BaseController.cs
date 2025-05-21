﻿using Fiap.Application.Common;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Claims;
using static Fiap.Domain.SeedWork.NotificationModel;

namespace Fiap.Api
{
    [ExcludeFromCodeCoverage]
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
            if (!IsValidOperation())
            {
                var statusCode = MapNotificationToStatusCode(_notification.NotificationModel.NotificationType);

                return StatusCode(statusCode, new
                {
                    success = false,
                    error = _notification.NotificationModel
                });
            }

            if (id == null)
                return Ok(new { success = true, data = response });

            var controller = ControllerContext.RouteData.Values["controller"]?.ToString();
            var version = RouteData.Values["version"]?.ToString();
            var location = $"/api/v{version}/{controller}/{id}";

            return Created(location, new { success = true, data = response ?? new object() });
        }

        private int MapNotificationToStatusCode(NotificationModel.ENotificationType notificationType)
        {
            return notificationType switch
            {
                NotificationModel.ENotificationType.InternalServerError => StatusCodes.Status500InternalServerError,
                NotificationModel.ENotificationType.BusinessRules => StatusCodes.Status409Conflict,
                NotificationModel.ENotificationType.NotFound => StatusCodes.Status404NotFound,
                NotificationModel.ENotificationType.Unauthorized => StatusCodes.Status401Unauthorized,
                NotificationModel.ENotificationType.BadRequestError => StatusCodes.Status400BadRequest,
                NotificationModel.ENotificationType.Default => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status400BadRequest
            };
        }


        protected int GetLoggedUser()
        {
            var userIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var user = userIdentity?.Claims.Where(c => c.Type == "id").FirstOrDefault();
            return user == null ? 0 : int.Parse(user.Value);
        }
    }
}
