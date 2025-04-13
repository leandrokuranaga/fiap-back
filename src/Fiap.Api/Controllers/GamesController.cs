using Fiap.Application.Games.Services;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GamesController(IGamesService gamesService, INotification notification) : BaseController(notification)
    {

    }
}
