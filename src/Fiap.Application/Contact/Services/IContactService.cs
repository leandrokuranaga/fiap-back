using Fiap.Application.Contact.Models.DTOs;
using Fiap.Application.Contact.Models.Request;

namespace Fiap.Application.Contact.Services
{
    public interface IContactService
    {
        Task<ContactResponse> GetById(int id);
        Task<ContactResponse> CreateAsync(CreateContactRequest request);
        Task Update(UpdateContactRequest request);
        Task<bool> UpdateAsync(UpdateContactRequest request);


    }
}
