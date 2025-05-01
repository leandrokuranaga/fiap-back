using Abp.Domain.Entities;
using Fiap.Domain.UserAggregate.Entities; 
using Fiap.Domain.PromotionAggregate;    
using Fiap.Domain.SeedWork.Exceptions;
using IAggregateRoot = Fiap.Domain.SeedWork.IAggregateRoot;
using Fiap.Domain.GameAggregate.ValueObjects;

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

            Name = name;
            Genre = new(genre);
            Price = new Price(price).Value;
            PromotionId = promotionId;
        }

        public Game() { }

        public string Name { get; set; }
        public Genre Genre { get; set; }
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
    }
}
