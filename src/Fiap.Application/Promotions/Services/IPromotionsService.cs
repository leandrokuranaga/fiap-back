using Fiap.Application.Promotions.Models.Request;
using Fiap.Application.Promotions.Models.Response;

namespace Fiap.Application.Promotions.Services
{
    public interface IPromotionsService
    {
        Task<CreatePromotionResponse> CreateAsync(CreatePromotionRequest request);
        Task<UpdatePromotionResponse> UpdateAsync(UpdatePromotionRequest request);
    }
}
