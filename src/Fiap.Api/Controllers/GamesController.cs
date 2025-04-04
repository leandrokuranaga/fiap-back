using Fiap.Application.Games.Services;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController(IGamesService gamesService, INotification notification) : BaseController(notification)
    {

    }
}
