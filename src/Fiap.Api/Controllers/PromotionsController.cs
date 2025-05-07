using Fiap.Application.Common;
using Fiap.Application.Promotions.Models.Request;
using Fiap.Application.Promotions.Models.Response;
using Fiap.Application.Promotions.Services;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Fiap.Api.Controllers
{
    /// <summary>
    /// Controller used by admin to manage all promotions for the games
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Roles = "Admin")]
    public class PromotionsController(IPromotionsService promotionsService, INotification notification) : BaseController(notification)
    {
        /// <summary>
        /// Creates a new promotion, optionally associated with one or more games.
        /// </summary>
        /// <param name="request">The promotion data to be created.</param>
        /// <returns>A response containing the created promotion.</returns>
        [HttpPost]
        [SwaggerOperation("Create a new Promotion for N or 0 games")]
        [ProducesResponseType(typeof(BaseResponse<PromotionResponse>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePromotionRequest request)
        {
            var result = await promotionsService.CreateAsync(request);
            return Response<PromotionResponse>(result?.PromotionId, result); 
        }

        /// <summary>
        /// Updates an existing promotion with new values, optionally updating associated games.
        /// </summary>
        /// <param name="id">The ID of the promotion to be updated (must be greater than 0).</param>
        /// <param name="request">The updated promotion data.</param>
        /// <returns>A response containing the updated promotion.</returns>
        [HttpPatch("{id:int:min(1)}")]
        [SwaggerOperation("Updates a value for a promotion for N or 0 games")]
        [ProducesResponseType(typeof(BaseResponse<PromotionResponse>), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<PromotionResponse>), (int)HttpStatusCode.NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdatePromotionRequest request)
        {
            var result = await promotionsService.UpdateAsync(id, request);
            return Response(BaseResponse<PromotionResponse>.Ok(null));
        }

        /// <summary>
        /// Get a promotion
        /// </summary>
        /// <param name="id">The ID of the promotion to be fetched (must be greater than 0).</param>
        /// <returns>A response containing the promotion.</returns>
        [SwaggerOperation("Updates a value for a promotion for N or 0 games")]
        [ProducesResponseType(typeof(BaseResponse<PromotionResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<PromotionResponse>), (int)HttpStatusCode.NotFound)]
        [ProducesDefaultResponseType]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await promotionsService.GetPromotionAsync(id);
            return Response(BaseResponse<PromotionResponse>.Ok(result));
        }
    }
}
