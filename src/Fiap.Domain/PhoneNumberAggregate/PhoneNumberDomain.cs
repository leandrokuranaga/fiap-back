using Abp.Domain.Entities;
using Fiap.Domain.ContactAggregate;

namespace Fiap.Domain.PhoneNumberAggregate
{
    public class PhoneNumberDomain : Entity
    {
        public PhoneNumberDomain(string phoneNumber, int contactId, string dDD, string state)
        {
            PhoneNumber = phoneNumber;
            ContactId = contactId;
            DDD = dDD;
            State = state;
        }

        public string PhoneNumber { get; set; }
        public int ContactId { get; set; }
        public string DDD { get; set; }
        public string State { get; set; }

        public virtual ContactDomain Contact { get; set; }
    }
}
