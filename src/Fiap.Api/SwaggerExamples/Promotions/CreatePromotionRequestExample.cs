using Fiap.Application.Promotions.Models.Request;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Api.SwaggerExamples.Promotions
{
    [ExcludeFromCodeCoverage]
    public class CreatePromotionRequestExample : IExamplesProvider<CreatePromotionRequest>
    {
        public CreatePromotionRequest GetExamples()
        {
            return new CreatePromotionRequest
            {
                Discount = 25,
                ExpirationDate = DateTime.UtcNow.AddDays(15),
                GameId = [1, 2, 3]
            };
        }
    }
}
