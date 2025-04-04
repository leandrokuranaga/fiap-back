using Abp.Domain.Entities;
using Fiap.Domain.LibraryAggregate;
using Fiap.Domain.LibraryGameAggregate;
using Fiap.Domain.PromotionAggregate;

namespace Fiap.Domain.GameAggregate
{
    public class GameDomain : Entity
    {
        public GameDomain(string name, string genre, double price, int? promotionId)
        {
            Name = name;
            Genre = genre;
            Price = price;
            PromotionId = promotionId;
        }

        public GameDomain()
        {
            
        }

        public string Name { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public int? PromotionId { get; set; }

        public virtual PromotionDomain Promotion { get; set; }
        public virtual ICollection<LibraryGameDomain> Libraries { get; set; }
    }
}
