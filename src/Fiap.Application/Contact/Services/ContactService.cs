using Fiap.Application.Common;
using Fiap.Application.Contact.Models.Request;
using Fiap.Domain.ContactAggregate;
using Fiap.Domain.SeedWork;
using Microsoft.Extensions.Caching.Memory;

namespace Fiap.Application.Contact.Services
{
    public class ContactService(INotification notification, IContactRepository contactRepository, IMemoryCache cache) : BaseService(notification), IContactService
    {
        public Task Create(CreateContactRequest request)
        {
            throw new NotImplementedException();
        }

        public Task Update(UpdateContactRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
