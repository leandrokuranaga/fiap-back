using Fiap.Application.Promotions.Models.Request;
using Fiap.Application.Promotions.Models.Response;

namespace Fiap.Application.Promotions.Services
{
    public interface IPromotionsService
    {
        Task<PromotionResponse> CreateAsync(CreatePromotionRequest request);
        Task<PromotionResponse> UpdateAsync(UpdatePromotionRequest request);
    }
}
