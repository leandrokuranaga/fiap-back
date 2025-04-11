using Fiap.Application.Common;
using Fiap.Application.Promotions.Models.Request;
using Fiap.Application.Promotions.Models.Response;

namespace Fiap.Application.Promotions.Services
{
    public interface IPromotionsService
    {
        Task<PromotionResponse> CreateAsync(CreatePromotionRequest request);
        Task<PromotionResponse> UpdateAsync(int id, UpdatePromotionRequest request);
        Task<BaseResponse<object>> DeleteAsync(int id);
    }
}
