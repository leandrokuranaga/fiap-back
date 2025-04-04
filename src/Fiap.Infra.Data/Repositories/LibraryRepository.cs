using Fiap.Domain.LibraryAggregate;
using Fiap.Infra.Data.Repositories.Base;

namespace Fiap.Infra.Data.Repositories
{
    public class LibraryRepository(IUnitOfWork unitOfWork) : BaseRepository<LibraryDomain>(unitOfWork), ILibraryRepository
    {
    }
}
