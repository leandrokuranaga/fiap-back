using Fiap.Application.Promotions.Models.Request;
using Swashbuckle.AspNetCore.Filters;

namespace Fiap.Api.SwaggerExamples.Promotions
{
    public class UpdatePromotionRequestExample : IExamplesProvider<UpdatePromotionRequest>
    {
        public UpdatePromotionRequest GetExamples()
        {
            return new UpdatePromotionRequest
            {
                Discount = 10,
                ExpirationDate = DateTime.UtcNow.AddDays(15),
                GameId = [1, 2, 3]
            };
        }
    }
}
