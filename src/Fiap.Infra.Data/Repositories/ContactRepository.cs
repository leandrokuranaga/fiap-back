using Fiap.Domain.ContactAggregate;
using Fiap.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Infra.Data.Repositories
{
    public class ContactRepository(IUnitOfWork unitOfWork) : BaseRepository<ContactDomain>(unitOfWork), IContactRepository
    {
        public async Task<ContactDomain> GetByIdAsync(int id)
        {
            return await unitOfWork.Context.Set<ContactDomain>()
                .Include(c => c.Emails)        // Carrega os emails relacionados
                .Include(c => c.PhoneNumbers) // Carrega os telefones relacionados
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(ContactDomain contact)
        {
            await unitOfWork.Context.Set<ContactDomain>().AddAsync(contact);
            await unitOfWork.CommitAsync();
        }

        public async Task InsertRangeAsync(IEnumerable<ContactDomain> contacts)
        {
            await unitOfWork.Context.Set<ContactDomain>().AddRangeAsync(contacts);
            await unitOfWork.CommitAsync();
        }

        public void Update(ContactDomain contact)
        {
            unitOfWork.Context.Set<ContactDomain>().Update(contact);
            unitOfWork.CommitAsync();
        }
    }
}
