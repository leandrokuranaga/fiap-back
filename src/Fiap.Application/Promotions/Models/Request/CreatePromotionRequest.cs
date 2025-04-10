using Fiap.Domain.PromotionAggregate;

namespace Fiap.Application.Promotions.Models.Request
{
    public class CreatePromotionRequest
    {
        public double Discount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public List<int?>? GameId { get; set; }

        public static explicit operator PromotionDomain(CreatePromotionRequest c)
        {
            return new PromotionDomain
            {
                Discount = c.Discount,
                EndDate = c.ExpirationDate,
                StartDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
            };
        }
    }
}
