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
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController(IPromotionsService promotionsService, INotification notification) : BaseController(notification)
    {
        [HttpPost]
        [SwaggerOperation("Create a new Promotion for N or 0 games")]
        [ProducesResponseType(typeof(BaseResponse<PromotionResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<PromotionResponse>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponse<PromotionResponse>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]

        public async Task<IActionResult> Create([FromBody] CreatePromotionRequest request)
        {
            var result = await promotionsService.CreateAsync(request);
            return Response(BaseResponse<PromotionResponse>.Ok(result));
        }


        [HttpPatch("{id:int:min(1)}")]
        [SwaggerOperation("Updates a value for a promotion for N or 0 games")]
        [ProducesResponseType(typeof(BaseResponse<PromotionResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<PromotionResponse>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponse<PromotionResponse>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePromotionRequest request)
        {
            var result = await promotionsService.UpdateAsync(id, request);
            return Response(BaseResponse<PromotionResponse>.Ok(result));
        }

        [HttpDelete("{id:int:min(1)}")]
        [SwaggerOperation("Delete a promotion by id")]
        [ProducesResponseType(typeof(EmptyResultModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<EmptyResultModel>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponse<EmptyResultModel>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        public async Task<IActionResult> Delete([FromBody] int id)
            => Response(await promotionsService.DeleteAsync(id));
    }
}
