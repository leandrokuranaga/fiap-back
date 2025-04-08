using Fiap.Domain.LibraryAggregate;

namespace Fiap.Infra.Data.MapEntities.Seeds
{
    public static class LibrarySeed
    {
        public static List<LibraryDomain> Libraries()
        {
            return
            [
                new LibraryDomain()
                {
                    Id = 1,
                    UserId = 1
                },
                new LibraryDomain()
                {
                    Id = 2,
                    UserId = 2
                },
            ];
        }
    }

}
