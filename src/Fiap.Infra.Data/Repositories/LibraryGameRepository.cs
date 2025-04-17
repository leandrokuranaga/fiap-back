using Fiap.Domain.LibraryGameAggregate;
using Fiap.Infra.Data.Repositories.Base;

namespace Fiap.Infra.Data.Repositories
{
    public class LibraryGameRepository(Context context) : BaseRepository<LibraryGameDomain>(context), ILibraryGameRepository
    {
    }
}
