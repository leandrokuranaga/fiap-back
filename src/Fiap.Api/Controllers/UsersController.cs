using Fiap.Application.Common;
using Fiap.Application.Users.Models.Request;
using Fiap.Application.Users.Models.Response;
using Fiap.Application.Users.Services;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController(IUsersService usersService, INotification notification) : BaseController(notification)
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request) 
        {
            var result = await usersService.Create(request);
            return Response(BaseResponse<UserResponse>.Ok(result));
        }


        [HttpPatch("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request)
        {
            var result = await usersService.Update(id, request);
            return Response(BaseResponse<UserResponse>.Ok(result));
        }


        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
        {
            await usersService.Delete(id);
            return Response(BaseResponse<EmptyResultModel>.Ok(new EmptyResultModel()));
        }

       
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await usersService.Get(id);
            return Response(BaseResponse<UserResponse>.Ok(result));
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await usersService.GetAll();
            return Response(BaseResponse<List<UserResponse>>.Ok(result));
        }

    }
}
