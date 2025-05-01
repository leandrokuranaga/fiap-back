using Abp.Domain.Entities;
using Fiap.Domain.GameAggregate;
using Fiap.Domain.SeedWork.Exceptions;
using IAggregateRoot = Fiap.Domain.SeedWork.IAggregateRoot;

namespace Fiap.Domain.PromotionAggregate
{
    public class Promotion : Entity, IAggregateRoot
    {
        public Promotion()
        {
        }

        public Promotion(double discount, DateTime startDate, DateTime endDate)
        {
            Discount = discount;
            StartDate = startDate;
            EndDate = endDate;
        }

        public double Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<Game> Games { get; set; } = [];
        public void UpdateDiscount(double? discount, DateTime? endDate)
        {
            if (discount.HasValue)
                Discount = discount.Value;

            if (endDate.HasValue)
                EndDate = endDate.Value;
        }

        public void ValidatePeriod()
        {
            if (EndDate <= StartDate)
                throw new BusinessRulesException("Promotion end date cannot be earlier than the start date.");
        }

        public bool IsExpired() => EndDate < DateTime.UtcNow;

        public bool IsActive() => StartDate <= DateTime.UtcNow && EndDate >= DateTime.UtcNow;

        public double GetDiscountedPrice(double originalPrice)
        {
            return originalPrice * (1 - Discount / 100);
        }
    }
}
