using Abp.Runtime.Caching;
using Fiap.Application.Common;
using Fiap.Application.Contact.Models.DTOs;
using Fiap.Application.Contact.Models.Request;
using Fiap.Domain.ContactAggregate;
using Fiap.Domain.SeedWork;
using Microsoft.Extensions.Caching.Memory;

namespace Fiap.Application.Contact.Services
{
    public class ContactService(INotification notification, IContactRepository contactRepository, IMemoryCache cache) : BaseService(notification), IContactService
    {
        public async Task<ContactResponse> GetById(int id)
        {
            if (id <= 0)
            {
                notification.AddNotification("ID Inválido", "O ID do contato não pode ser zero ou negativo.", NotificationModel.ENotificationType.BadRequestError);
                return null;
            }

            if (!cache.TryGetValue(id, out ContactDomain contact))
            {
                contact = await contactRepository.GetByIdAsync(id);
                if (contact == null)
                {
                    notification.AddNotification("Contato Não Encontrado", "Contato não encontrado.", NotificationModel.ENotificationType.NotFound);
                    return null;
                }
                cache.Set(id, contact, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5)));
            }

            // Transformando para DTO antes de retornar
            return new ContactResponse
            {
                Id = contact.Id,
                Name = contact.Name,
                Emails = contact.Emails?.Select(e => new EmailResponse
                {
                    Id = e.Id,
                    Email = e.Email
                }).ToList(),

                PhoneNumbers = contact.PhoneNumbers?.Select(p => new PhoneNumberResponse
                {
                    Id = p.Id,
                    PhoneNumber = p.PhoneNumber
                }).ToList()
            };
        }

        public async Task Create(CreateContactRequest request)
        {
            var contact = new ContactDomain(request.Name);
            await contactRepository.AddAsync(contact);
        }

        public async Task<ContactResponse> CreateAsync(CreateContactRequest request)
        {
            var contact = new ContactDomain(request.Name);  // Agora sem Id
            await contactRepository.AddAsync(contact);

            return new ContactResponse
            {
                Id = contact.Id, // O banco de dados gerará automaticamente o Id
                Name = contact.Name
            };
        }

        public async Task Update(UpdateContactRequest request)
        {
            var contact = await contactRepository.GetByIdAsync(request.Id);
            if (contact == null)
            {
                notification.AddNotification("Contato Não Encontrado", "Contato não encontrado.", NotificationModel.ENotificationType.NotFound);
                return;
            }

            // Atualiza os dados sem recriar o objeto
            contact.Name = request.Name;
            contactRepository.Update(contact);
        }

        public async Task<bool> UpdateAsync(UpdateContactRequest request)
        {
            var contact = await contactRepository.GetByIdAsync(request.Id);
            if (contact == null)
            {
                notification.AddNotification("Contato Não Encontrado", "Contato não encontrado.", NotificationModel.ENotificationType.NotFound);
                return false;
            }

            contact.Name = request.Name;

            // Chama o método do repositório base que já contém a lógica de update e commit
            await contactRepository.UpdateAsync(contact);

            return true;
        }
    }
}
