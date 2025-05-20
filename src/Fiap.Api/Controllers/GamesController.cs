using Fiap.Application.Common;
using Fiap.Application.Games.Models.Request;
using Fiap.Application.Games.Models.Response;
using Fiap.Application.Games.Services;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace Fiap.Api.Controllers
{
    /// <summary>
    /// Controller used to manage game operations, such as creation, retrieval and management
    /// </summary>
    [ApiController]
    [Authorize(Roles = "Admin")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GamesController(IGamesService gamesService, INotification notification) : BaseController(notification)
    {
        /// <summary>
        /// Creates a new game.
        /// </summary>
        /// <param name="request">The game data required to create a new entry in the system.</param>
        /// <returns>A response containing the created game, or an error message if the input is invalid.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new game.",
            Description = "Creates a new game entry using the provided data, including title, genre, price, and promotion (if applicable). " +
                          "Returns the created game on success. Returns a 400 if the input is invalid, " +
                          "409 if a duplicate or conflicting game exists, 401/403 if unauthorized, and 500 in case of server error."
        )]
        [ProducesResponseType(typeof(SuccessResponse<GameResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status400BadRequest)]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(GenericErrorBadRequestExample))]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status409Conflict)]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(GenericErrorConflictExample))]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status401Unauthorized)]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(GenericErrorUnauthorizedExample))]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status403Forbidden)]
        [SwaggerResponseExample(StatusCodes.Status403Forbidden, typeof(GenericErrorForbiddenExample))]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(GenericErrorInternalServerExample))]
        public async Task<IActionResult> Create([FromBody] CreateGameRequest request)
        {
            var result = await gamesService.CreateAsync(request);
            return Response(BaseResponse<GameResponse>.Ok(result));
        }

        /// <summary>
        /// Retrieve all games.
        /// </summary>
        /// <returns>A response containing a list of all games, or an empty list if no games are found.</returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Retrieve all games.",
            Description = "Returns a list of all available games in the system. " +
                          "If no games exist, an empty list is returned. " +
                          "Requires authorization. If the user is unauthorized or does not have permission, a 401 or 403 error is returned."
        )]
        [ProducesResponseType(typeof(SuccessResponse<IEnumerable<GameResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status401Unauthorized)]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(GenericErrorUnauthorizedExample))]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status403Forbidden)]
        [SwaggerResponseExample(StatusCodes.Status403Forbidden, typeof(GenericErrorForbiddenExample))]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(GenericErrorInternalServerExample))]
        public async Task<IActionResult> GetAll()
        {
            var result = await gamesService.GetAllAsync();
            return Response(BaseResponse<IEnumerable<GameResponse>>.Ok(result));
        }

        /// <summary>
        /// Retrieve a game by ID.
        /// </summary>
        /// <param name="id">The id game data required to fetch.</param>
        /// <returns>A response containing a game, or an empty list if no game are found.</returns>
        [HttpGet("{id:int:min(1)}")]
        [SwaggerOperation(
            Summary = "Retrieve a game by ID.",
            Description = "Fetches a single game based on the provided ID. " +
                          "If a game with the given ID exists, it returns its full information. " +
                          "If no game is found, a 404 response is returned. " +
                          "Requires authorization. Unauthorized or forbidden access will return 401 or 403, respectively."
        )]
        [ProducesResponseType(typeof(SuccessResponse<GameResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status404NotFound)]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(GenericErrorNotFoundExample))]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status401Unauthorized)]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(GenericErrorUnauthorizedExample))]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status403Forbidden)]
        [SwaggerResponseExample(StatusCodes.Status403Forbidden, typeof(GenericErrorForbiddenExample))]
        [ProducesResponseType(typeof(BaseResponse<object>), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(GenericErrorInternalServerExample))]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await gamesService.GetAsync(id);
            return Response(BaseResponse<GameResponse>.Ok(result));
        }
    }
}
