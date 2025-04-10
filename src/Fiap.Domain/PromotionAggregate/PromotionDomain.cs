using Abp.Domain.Entities;
using Fiap.Domain.GameAggregate;

namespace Fiap.Domain.PromotionAggregate
{
    public class PromotionDomain : Entity
    {
        public PromotionDomain()
        {
        }

        public PromotionDomain(double discount, DateTime startDate, DateTime endDate)
        {
            Discount = discount;
            StartDate = startDate;
            EndDate = endDate;

            ValidatePeriod();

        }

        public double Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<GameDomain> Games { get; set; } = [];

        public void UpdateDiscount(double? discount, DateTime? endDate)
        {
            if (discount.HasValue)
                Discount = discount.Value;

            if (endDate.HasValue)
                EndDate = endDate.Value;

            ValidatePeriod();
        }

        public void ValidatePeriod()
        {
            if (EndDate <= StartDate)
                throw new InvalidOperationException("Promotion end date cannot be earlier than the start date.");
        }

        public bool IsExpired() => EndDate < DateTime.UtcNow;

        public bool IsActive() => StartDate <= DateTime.UtcNow && EndDate >= DateTime.UtcNow;

        public double GetDiscountedPrice(double originalPrice)
        {
            return originalPrice * (1 - Discount / 100);
        }



    }
}
