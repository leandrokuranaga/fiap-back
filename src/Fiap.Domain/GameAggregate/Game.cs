using Abp.Domain.Entities;
using Fiap.Domain.UserAggregate.Entities;

namespace Fiap.Domain.Game
{
    public class Game : Entity
    {
        public Game(int id, string name, string genre, double price, int? promotionId)
        {
            Id = id;
            Name = name;
            Genre = genre;
            Price = price;
            PromotionId = promotionId;
        }

        public Game()
        {
            
        }

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

    }
}
