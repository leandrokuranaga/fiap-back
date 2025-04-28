using Fiap.Application.Common;
using Fiap.Application.Users.Models.Request;
using Fiap.Application.Users.Models.Response;
using Fiap.Application.Users.Services;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Controllers
{
    /// <summary>
    /// Controller responsible for user management operations such as creating, updating, retrieving, and deleting users.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController(IUsersService usersService, INotification notification) : BaseController(notification)
    {
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="request">The details of the user to create.</param>
        /// <returns>The created user's information.</returns>
        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserRequest request) 
        {
            var result = await usersService.CreateAsync(request);
            return Response(BaseResponse<UserResponse>.Ok(result));
        }

        /// <summary>
        /// Creates a new user (admin or regular user, active or not)
        /// </summary>
        /// <param name="request">The details of the admin user to create.</param>
        /// <returns>The new user created information.</returns>
        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdminAsync([FromBody] CreateUserAdminRequest request)
        {
            var result = await usersService.CreateAdminAsync(request);
            return Response(BaseResponse<UserResponse>.Ok(result));
        }

        [HttpPatch("{id:int:min(1)}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateUserRequest request)
        {
            var result = await usersService.UpdateAsync(id, request);
            return Response(BaseResponse<UserResponse>.Ok(result));
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await usersService.DeleteAsync(id);
            return Response(BaseResponse<EmptyResultModel>.Ok(new EmptyResultModel()));
        }
       
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await usersService.GetAsync(id);
            return Response(BaseResponse<UserResponse>.Ok(result));
        }
       
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await usersService.GetAllAsync();
            return Response(BaseResponse<List<UserResponse>>.Ok(result));
        }

    }
}
