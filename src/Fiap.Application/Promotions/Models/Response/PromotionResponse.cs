using Fiap.Application.Common;
using Fiap.Domain.PromotionAggregate;

namespace Fiap.Application.Promotions.Models.Response
{
    public class PromotionResponse
    {
        public int PromotionId { get; set; }
        public double Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public static explicit operator PromotionResponse(PromotionDomain promotion)
        {
            return new PromotionResponse
            {
                PromotionId = promotion.Id,
                Discount = promotion.Discount,
                StartDate = promotion.StartDate,
                EndDate = promotion.EndDate
            };
        }
    }
}
