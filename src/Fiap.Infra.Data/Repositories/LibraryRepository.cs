using Fiap.Domain.LibraryAggregate;
using Fiap.Infra.Data.Repositories.Base;

namespace Fiap.Infra.Data.Repositories
{
    public class LibraryRepository(Context context) : BaseRepository<LibraryDomain>(context), ILibraryRepository
    {
    }
}
