using Abp.Domain.Entities;
using Fiap.Domain.ContactAggregate;

namespace Fiap.Domain.EmailAggregate
{
    public class EmailDomain : Entity
    {
        public string Email { get; set; }
        public int ContactId { get; set; }

        public EmailDomain(string email, int contactId)
        {
            Email = email;
            ContactId = contactId;
        }

        public virtual ContactDomain Contact { get; set; }
    }
}
