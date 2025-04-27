using Fiap.Application.Common;
using Fiap.Application.Auth.Services;
using Fiap.Application.Auth.Models;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace Fiap.Api.Controllers
{
    /// <summary>
    /// Controller responsible for authentication operations such as user login.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController(IAuthService authService, INotification notification) : BaseController(notification)
    {
        /// <summary>
        /// Authenticates a user and generates a JWT token upon successful login.
        /// </summary>
        /// <param name="request">The login request containing username and password.</param>
        /// <returns>Returns a JWT token if authentication is successful; otherwise, returns an error.</returns>
        [HttpPost("login")]
        [SwaggerOperation("Authenticate a user and get a JWT token.")]
        [ProducesResponseType(typeof(BaseResponse<LoginResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> LoginAsync([FromBody] Application.Auth.Models.LoginRequest request)
        {
            var result = await authService.LoginAsync(request);
            if (result == null)
            {
                notification.AddNotification("Login Failed", "Invalid username or password.", NotificationModel.ENotificationType.Unauthorized);
                return Unauthorized(BaseResponse<LoginResponse>.Fail(notification.NotificationModel));
            }

            return Response(BaseResponse<LoginResponse>.Ok(result));
        }
    }
}
