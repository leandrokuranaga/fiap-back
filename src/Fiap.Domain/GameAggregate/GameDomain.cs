using Abp.Domain.Entities;
using Fiap.Domain.LibraryGameAggregate;
using Fiap.Domain.PromotionAggregate;
using Fiap.Domain.SeedWork.Exceptions;

namespace Fiap.Domain.GameAggregate
{
    public class GameDomain : Entity
    {
        public GameDomain(int id, string name, string genre, double price, int? promotionId)
        {
            Id = id;
            Name = name;
            Genre = genre;
            PromotionId = promotionId;

            ValidatePrice(price);
            Price = price;
        }

        public GameDomain(string name, string genre, double price, int? promotionId)
        {
            Name = name;
            Genre = genre;
            PromotionId = promotionId;

            ValidatePrice(price);
            Price = price;
        }

        public GameDomain() { }

        public string Name { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public int? PromotionId { get; set; }

        public virtual PromotionDomain Promotion { get; set; }
        public virtual ICollection<LibraryGameDomain> Libraries { get; set; }

        public void AssignPromotion(int promotionId)
        {
            PromotionId = promotionId;
        }

        private void ValidatePrice(double price)
        {
            if (price < 0)
                throw new BusinessRulesException("The price of the game must be greater than or equal to 0.");
        }
    }
}
