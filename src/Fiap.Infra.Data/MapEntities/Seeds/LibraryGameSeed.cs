using Fiap.Domain.Common.ValueObjects;
using Fiap.Domain.UserAggregate.Entities;

namespace Fiap.Infra.Data.MapEntities.Seeds
{
    public static class LibraryGameSeed
    {
        public static List<LibraryGame> LibraryGames()
        {
            return new List<LibraryGame>
            {
                new LibraryGame
                {
                    Id = 1,
                    UserId = 1,
                    GameId = 1,
                    PurchaseDate = new UtcDate(DateTime.SpecifyKind(new DateTime(2025, 04, 01), DateTimeKind.Utc)),
                //    PricePaid = new Money(10.00, "USD")
                },
                new LibraryGame
                {
                    Id = 2,
                    UserId = 2,
                    GameId = 2,
                    PurchaseDate = new UtcDate(DateTime.SpecifyKind(new DateTime(2025, 05, 01), DateTimeKind.Utc)),
                  //  PricePaid = new Money(15.00, "USD")
                },
                new LibraryGame
                {
                    Id = 3,
                    UserId = 1,
                    GameId = 3,
                    PurchaseDate = new UtcDate(DateTime.SpecifyKind(new DateTime(2025, 06, 01), DateTimeKind.Utc)),
                    //PricePaid = new Money(20.00, "USD")
                },
                new LibraryGame
                {
                    Id = 4,
                    UserId = 2,
                    GameId = 4,
                    PurchaseDate = new UtcDate(DateTime.SpecifyKind(new DateTime(2025, 07, 01), DateTimeKind.Utc)),
                    //PricePaid = new Money(28.99, "USD")
                }
            };
        }
    }
}
