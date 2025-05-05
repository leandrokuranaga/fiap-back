using Fiap.Application.Common;
using Fiap.Application.Library.Models.Response;
using Fiap.Application.Library.Services;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Fiap.Api.Controllers
{
    /// <summary>
    /// Controller used to manage libraries
    /// </summary>
    /// <param name="libraryGameService"></param>
    /// <param name="notification"></param>
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LibraryGameController(ILibraryService libraryGameService, INotification notification) : BaseController(notification)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>A response containing a library of games from the logged in user</returns>
        [HttpGet]
        [SwaggerOperation("Get a Library of Games from user logged in")]
        [ProducesResponseType(typeof(BaseResponse<LibraryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllByUserAsync()
        {
            var userId = Convert.ToInt32(GetLoggedUser());
            var result = await libraryGameService.GetAllByUserLoggedAsync(userId);
            return Response(BaseResponse<List<LibraryResponse>>.Ok(result));
        }
    }
}
