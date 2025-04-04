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
        }

        public double Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<GameDomain> Games { get; set; }

    }
}
