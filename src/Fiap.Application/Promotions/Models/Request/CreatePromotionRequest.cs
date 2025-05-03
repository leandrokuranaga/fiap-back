using Fiap.Domain.Common.ValueObjects;
using Fiap.Domain.PromotionAggregate;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Application.Promotions.Models.Request
{
    [ExcludeFromCodeCoverage]
    public record CreatePromotionRequest
    {
        public double Discount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public List<int?>? GameId { get; set; }

        public static explicit operator Promotion(CreatePromotionRequest c)
        {
            return new Promotion
            {
                Discount = new Money(c.Discount),
                EndDate = new UtcDate(c.ExpirationDate),
                StartDate = new UtcDate(DateTime.UtcNow),
            };
        }
    }
}
