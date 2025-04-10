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
        [ProducesResponseType(typeof(PromotionResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] CreatePromotionRequest request)
            => Response(await promotionsService.CreateAsync(request));


        [HttpPatch("{id:int}")]
        [SwaggerOperation("Updates a value for a promotion for N or 0 games")]
        [ProducesResponseType(typeof(PromotionResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePromotionRequest request)
            => Response(await promotionsService.UpdateAsync(id, request));
    }
}
