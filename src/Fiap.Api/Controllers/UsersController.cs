using Fiap.Application.Common;
using Fiap.Application.Users.Models.Request;
using Fiap.Application.Users.Models.Response;
using Fiap.Application.Users.Services;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Fiap.Api.Controllers
{
    /// <summary>
    /// Controller used to manage all users
    /// </summary>
    [Authorize(Roles = "Admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController(IUsersService usersService, INotification notification) : BaseController(notification)
    {
        [AllowAnonymous]
        [HttpPost("create")]
        [SwaggerOperation("Creates a regular user")]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserRequest request)
        {
            var result = await usersService.CreateAsync(request);
            return Response<UserResponse>(result.UserId, result);
        }

        /// <summary>
        /// Creates a new user (admin or regular user, active or not)
        /// </summary>
        /// <param name="request">The details of the user to create (only admin).</param>
        /// <returns>The new user created information.</returns>
        [HttpPost("create-admin")]
        [SwaggerOperation("Creates a user (only admin)")]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateAdminAsync([FromBody] CreateUserAdminRequest request)
        {
            var result = await usersService.CreateAdminAsync(request);
            return Response<UserResponse>(result.UserId, result);
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="request">The details of the user to be updated.</param>
        /// <param name="id">id of the updated user</param>
        /// <returns>The new user updated information.</returns>
        [SwaggerOperation("Updates a user")]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        [HttpPatch("{id:int:min(1)}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateUserRequest request)
        {
            await usersService.UpdateAsync(id, request);
            return Response(BaseResponse<UserResponse>.Ok(null));
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id">id of the deleted user</param>
        /// <returns>The deleted user information.</returns>
        [SwaggerOperation("Delete a user based on id")]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await usersService.DeleteAsync(id);
            return Response(BaseResponse<UserResponse>.Ok(null));
        }

        /// <summary>
        /// Gets a user based on the id
        /// </summary>
        /// <param name="id">Gets a user based on the id.</param>
        /// <returns>The user information.</returns>
        [SwaggerOperation("Get a user based on id")]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await usersService.GetAsync(id);
            return Response(BaseResponse<UserResponse>.Ok(result));
        }

        /// <summary>
        /// Gets a list of users
        /// </summary>
        /// <returns>The list of users information.</returns>
        [SwaggerOperation("Gets a list of users")]
        [ProducesResponseType(typeof(BaseResponse<UserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await usersService.GetAllAsync();
            return Response(BaseResponse<List<UserResponse>>.Ok(result));
        }

    }
}
