using Fiap.Application.Users.Models.Response;
using Fiap.Domain.UserAggregate.Entities;

namespace Fiap.Application.Library.Models.Response
{
    public class LibraryResponse
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double PricePaid { get; set; }

        public static explicit operator LibraryResponse(LibraryGame library)
        {
            return new LibraryResponse
            {
                GameId = library.GameId,
                UserId = library.Id,
                PurchaseDate = library.PurchaseDate,
                PricePaid = library.PricePaid
            };
        }
    }
}
