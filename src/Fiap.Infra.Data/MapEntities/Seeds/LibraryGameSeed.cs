using Fiap.Domain.UserAggregate.Entities;

namespace Fiap.Infra.Data.MapEntities.Seeds
{
    public static class LibraryGameSeed
    {
        public static List<LibraryGame> LibraryGames()
        {
            return [
                new LibraryGame(1, 1, 1, DateTime.SpecifyKind(new DateTime(2024, 07, 01), DateTimeKind.Utc), 200),
                new LibraryGame(2, 1, 2, DateTime.SpecifyKind(new DateTime(2022, 03, 09), DateTimeKind.Utc), 50),
                new LibraryGame(3, 1, 3, DateTime.SpecifyKind(new DateTime(2020, 11, 22), DateTimeKind.Utc), 199),
                new LibraryGame(4, 1, 4, DateTime.SpecifyKind(new DateTime(2019, 05, 03), DateTimeKind.Utc), 60),
            ];
        }
    }
}
