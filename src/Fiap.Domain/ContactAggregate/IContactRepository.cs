using Fiap.Domain.SeedWork;

namespace Fiap.Domain.ContactAggregate
{
    public interface IContactRepository : IBaseRepository<ContactDomain>
    {
        Task<ContactDomain> GetByIdAsync(int id);
        Task AddAsync(ContactDomain contact);
        void Update(ContactDomain contact);
    }
}
