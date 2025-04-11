using Fiap.Application.Common;
using Fiap.Application.Promotions.Models.Request;
using Fiap.Application.Promotions.Models.Response;
using Fiap.Application.Promotions.Services;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Fiap.Api.Controllers
{
    /// <summary>
    /// Controller responsible for managing promotions and related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController(IPromotionsService promotionsService, INotification notification) : BaseController(notification)
    {
        /// <summary>
        /// Creates a new promotion, optionally associated with one or more games.
        /// </summary>
        /// <param name="request">The promotion data to be created.</param>
        /// <returns>A response containing the created promotion.</returns>
        [HttpPost]
        [SwaggerOperation("Create a new Promotion for N or 0 games")]
        [ProducesResponseType(typeof(BaseResponse<PromotionResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreatePromotionRequest request)
        {
            var result = await promotionsService.CreateAsync(request);
            return Response(BaseResponse<PromotionResponse>.Ok(result));
        }

        /// <summary>
        /// Updates an existing promotion with new values, optionally updating associated games.
        /// </summary>
        /// <param name="id">The ID of the promotion to be updated (must be greater than 0).</param>
        /// <param name="request">The updated promotion data.</param>
        /// <returns>A response containing the updated promotion.</returns>
        [HttpPatch("{id:int:min(1)}")]
        [SwaggerOperation("Updates a value for a promotion for N or 0 games")]
        [ProducesResponseType(typeof(BaseResponse<PromotionResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<PromotionResponse>), (int)HttpStatusCode.NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePromotionRequest request)
        {
            var result = await promotionsService.UpdateAsync(id, request);
            return Response(BaseResponse<PromotionResponse>.Ok(result));
        }
    }
}
