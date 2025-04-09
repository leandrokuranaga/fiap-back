using Fiap.Application.Users.Models.Request;
using Fiap.Application.Users.Services;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUsersService usersService, INotification notification) : BaseController(notification)
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
            => Response(await usersService.Create(request));

        
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
            => Response(await usersService.Update(request));

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteUserRequest { UserId = id };
            return Response(await usersService.Delete(request));
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await usersService.Get(id);
            return Response(result);
        }

       
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await usersService.GetAll();
            return Response(result);
        }

    }
}
