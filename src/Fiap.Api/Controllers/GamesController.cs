using Fiap.Application.Games.Models.Requests;
using Fiap.Application.Games.Services;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Fiap.Application.Games.Models.Responses;

namespace Fiap.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController(IGamesService gamesService, INotification notification) : BaseController(notification)
    {
        private readonly IGamesService _gamesService = gamesService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGameRequest request)
        {
            var result = await _gamesService.CreateAsync(request);

            return Response<GameResponse>(result);
        }
    }
}
