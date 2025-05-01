using Abp.Domain.Entities;
using Fiap.Domain.UserAggregate.Entities; 
using Fiap.Domain.PromotionAggregate;    
using Fiap.Domain.SeedWork.Exceptions;
using IAggregateRoot = Fiap.Domain.SeedWork.IAggregateRoot;
using Fiap.Domain.Common.ValueObjects;

namespace Fiap.Domain.GameAggregate
{
    public class Game : Entity, IAggregateRoot
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

            Name = name;
            Genre = genre;
            Price = new Money(price, "BRL").Value;
            PromotionId = promotionId;
        }

        public Game() { }

        public string Name { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public int? PromotionId { get; set; }

        public virtual Promotion Promotion { get; set; }
        public virtual ICollection<LibraryGame> Libraries { get; set; }

        public void AssignPromotion(int promotionId)
        {
            PromotionId = promotionId;
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
