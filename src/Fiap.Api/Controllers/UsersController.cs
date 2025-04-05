using Fiap.Application.Users.Models.Request;
using Fiap.Application.Users.Services;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUsersService usersService, INotification notification) : BaseController(notification)
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
            => Response(await usersService.Create(request));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
            => Response(await usersService.Update(request));

    }
}
