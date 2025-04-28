using Abp.Domain.Entities;
using Fiap.Domain.UserAggregate.Entities; 
using Fiap.Domain.Promotion;    
using Fiap.Domain.SeedWork.Exceptions;

namespace Fiap.Domain.Game
{
    public class Game : Entity
    {
        public Game(int id, string name, string genre, double price, int? promotionId)
            : this(name, genre, price, promotionId)
        {
            Id = id;
        }

        public Game(string name, string genre, double price, int? promotionId)
        {
            ValidateName(name);
            ValidateGenre(genre);
            ValidatePrice(price);

            Name = name;
            Genre = genre;
            Price = price;
            PromotionId = promotionId;
        }

        public Game() { }

        public string Name { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public int? PromotionId { get; set; }

        public virtual Promotion.Promotion Promotion { get; set; }
        public virtual ICollection<LibraryGame> Libraries { get; set; }

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
