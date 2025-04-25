using Abp.Domain.Entities;
using Fiap.Domain.LibraryGameAggregate;
using Fiap.Domain.PromotionAggregate;
using Fiap.Domain.SeedWork.Exceptions;

namespace Fiap.Domain.GameAggregate
{
    public class GameDomain : Entity
    {
        public GameDomain(int id, string name, string genre, double price, int? promotionId)
            : this(name, genre, price, promotionId)
        {
            Id = id;
        }

        public GameDomain(string name, string genre, double price, int? promotionId)
        {
            ValidateName(name);
            ValidateGenre(genre);
            ValidatePrice(price);

            Name = name;
            Genre = genre;
            Price = price;
            PromotionId = promotionId;
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
            if (double.IsNaN(price))
                throw new BusinessRulesException("The price of the game cannot be NaN.");

            if (price < 0)
                throw new BusinessRulesException("The price of the game must be greater than or equal to 0.");
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BusinessRulesException("The name of the game is required.");
        }

        private void ValidateGenre(string genre)
        {
            if (string.IsNullOrWhiteSpace(genre))
                throw new BusinessRulesException("The genre of the game is required.");
        }
    }
}
