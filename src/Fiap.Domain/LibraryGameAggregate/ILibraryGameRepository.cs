using Fiap.Infra.Data;
using Fiap.Domain.SeedWork;

namespace Fiap.Domain.LibraryGameAggregate
{
    public interface ILibraryGameRepository : IBaseRepository<LibraryGameDomain>, IUnitOfWork
    {
    }
}
