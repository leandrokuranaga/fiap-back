using Abp.Domain.Entities;
using Fiap.Domain.Game;

namespace Fiap.Domain.UserAggregate.Entities
{
    public class LibraryGame : Entity
    {
        public LibraryGame()
        {            
        }

        public LibraryGame(int id, int userId, int gameId, DateTime purchaseDate, double pricePaid)
        {
            Id = id;
            UserId = userId;
            GameId = gameId;
            PurchaseDate = purchaseDate;
            PricePaid = pricePaid;
        }

        public int GameId { get; set; }
        public int UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double PricePaid { get; set; }

        public virtual Game.Game Game { get; set; }
        public virtual User User { get; set; }
    }
}
