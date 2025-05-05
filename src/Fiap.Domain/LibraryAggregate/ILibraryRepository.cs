using Fiap.Domain.SeedWork;
using Fiap.Domain.UserAggregate.Entities;
using Fiap.Infra.Data;

namespace Fiap.Domain.LibraryAggregate
{
    public interface ILibraryRepository : IBaseRepository<LibraryGame>, IUnitOfWork
    {
    }
}
