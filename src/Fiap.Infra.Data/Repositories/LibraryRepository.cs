using Fiap.Domain.LibraryAggregate;
using Fiap.Domain.UserAggregate.Entities;
using Fiap.Infra.Data.Repositories.Base;

namespace Fiap.Infra.Data.Repositories
{
    public class LibraryRepository(Context context) : BaseRepository<LibraryGame>(context), ILibraryRepository
    {

    }
}
