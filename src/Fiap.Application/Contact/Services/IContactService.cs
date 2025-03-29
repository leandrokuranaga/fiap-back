using Fiap.Application.Contact.Models.Request;

namespace Fiap.Application.Contact.Services
{
    public interface IContactService
    {
        Task Create(CreateContactRequest request);
        Task Update(UpdateContactRequest request);
    }
}
