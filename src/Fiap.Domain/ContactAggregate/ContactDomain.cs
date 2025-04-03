using Abp.Domain.Entities;
using Fiap.Domain.EmailAggregate;
using Fiap.Domain.PhoneNumberAggregate;

namespace Fiap.Domain.ContactAggregate
{
    public class ContactDomain : Entity
    {
        public string? Name { get; set; }

        public ContactDomain(string? name)
        {
            Name = name;
        }

        public ContactDomain(int id, string? name)
        {
            Id = id;
            Name = name;
        }

        public virtual ICollection<EmailDomain> Emails { get; set; }    
        public virtual ICollection<PhoneNumberDomain> PhoneNumbers { get; set; }
    }
}
