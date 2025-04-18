using Fiap.Infra.Data;
using Fiap.Domain.SeedWork;

namespace Fiap.Domain.LibraryAggregate
{
    public interface ILibraryRepository : IBaseRepository<LibraryDomain>, IUnitOfWork
    {
    }
}
