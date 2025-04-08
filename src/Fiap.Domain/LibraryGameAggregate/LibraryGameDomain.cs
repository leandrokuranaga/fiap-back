using Abp.Domain.Entities;
using Fiap.Domain.GameAggregate;
using Fiap.Domain.LibraryAggregate;
using System;

namespace Fiap.Domain.LibraryGameAggregate
{
    public class LibraryGameDomain : Entity
    {
        public LibraryGameDomain()
        {
            
        }

        public LibraryGameDomain(int id, int libraryId, int gameId, DateTime purchaseDate, double pricePaid)
        {
            Id = id;
            LibraryId = libraryId;
            GameId = gameId;
            PurchaseDate = purchaseDate;
            PricePaid = pricePaid;
        }

        public int LibraryId { get; set; }
        public int GameId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double PricePaid { get; set; }

        public virtual GameDomain Game { get; set; }
        public virtual LibraryDomain Library { get; set; }
    }
}
