using Abp.Domain.Entities;
using Fiap.Domain.Common.ValueObjects;
using Fiap.Domain.GameAggregate;

namespace Fiap.Domain.UserAggregate.Entities
{
    public class LibraryGame : Entity
    {
        public LibraryGame()
        {            
        }

        public LibraryGame(int id, int userId, int gameId, DateTime purchaseDate, decimal pricePaid)
        {
            Id = id;
            UserId = userId;
            GameId = gameId;
            PurchaseDate = new UtcDate(purchaseDate);
            PricePaid = new Money(pricePaid);
        }

        public int GameId { get; set; }
        public int UserId { get; set; }
        public UtcDate PurchaseDate { get; set; }
        public Money PricePaid { get; set; }

        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
    }
}
