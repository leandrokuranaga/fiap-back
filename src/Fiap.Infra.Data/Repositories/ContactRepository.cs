using Fiap.Domain.ContactAggregate;
using Fiap.Infra.Data.Repositories.Base;

namespace Fiap.Infra.Data.Repositories
{
    public class ContactRepository(IUnitOfWork unitOfWork) : BaseRepository<ContactDomain>(unitOfWork), IContactRepository
    {
    }
}
