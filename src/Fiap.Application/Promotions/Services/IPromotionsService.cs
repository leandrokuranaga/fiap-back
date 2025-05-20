using Fiap.Application.Common;
using Fiap.Application.Promotions.Models.Request;
using Fiap.Application.Promotions.Models.Response;

namespace Fiap.Application.Promotions.Services
{
    public interface IPromotionsService
    {
        Task<PromotionResponse> CreateAsync(CreatePromotionRequest request);
        Task<BaseResponse<object>> UpdateAsync(int id, UpdatePromotionRequest request);
        Task<PromotionResponse> GetPromotionAsync(int id);
    }
}
