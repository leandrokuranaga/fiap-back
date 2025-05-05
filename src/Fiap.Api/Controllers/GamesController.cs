using Fiap.Application.Common;
using Fiap.Application.Games.Models.Request;
using Fiap.Application.Games.Models.Response;
using Fiap.Application.Games.Services;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Fiap.Api.Controllers
{
    /// <summary>
    /// Controller used to manage game operations, such as creation, retrieval and management
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GamesController(IGamesService gamesService, INotification notification) : BaseController(notification)
    { 
        /// <summary>
        /// Creates a new game with the provided information, such as title, genre, and price.
        /// </summary>
        /// <param name="request">The game data required to create a new entry in the system.</param>
        /// <returns>A response containing the created game, or an error message if the input is invalid.</returns>
        [HttpPost]
        [SwaggerOperation("Creates a new game")]
        [ProducesResponseType(typeof(BaseResponse<GameResponse>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateGameRequest request)
        {
            var result = await gamesService.CreateAsync(request);
            return Response(BaseResponse<GameResponse>.Ok(result));
        }

        /// <summary>
        /// Retrieves a list of all registered games, including their main details like title, genre, and price.
        /// </summary>
        /// <returns>A response containing a list of all games, or an empty list if no games are found.</returns>
        [HttpGet]
        [SwaggerOperation("Gets all games")]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<GameResponse>>), (int)HttpStatusCode.OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAll()
        {
            var result = await gamesService.GetAllAsync();
            return Response(BaseResponse<IEnumerable<GameResponse>>.Ok(result));
        }

        /// <summary>
        /// Retrieves a game
        /// </summary>
        /// <param name="id">The id game data required to fetch.</param>
        /// <returns>A response containing a game, or an empty list if no game are found.</returns>
        [HttpGet("{id:int:min(1)}")]
        [SwaggerOperation("Get game")]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<GameResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await gamesService.GetAsync(id);
            return Response(BaseResponse<GameResponse>.Ok(result));
        }

    }
}
