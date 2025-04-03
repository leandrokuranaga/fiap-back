using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Application.Contact.Models.Request
{
    public class CreateContactRequest
    {
        public string Name { get; set; }
        public List<EmailDto> Emails { get; set; }
        public List<PhoneNumberDto> PhoneNumbers { get; set; }
    }

    public class EmailDto
    {
        public string Email { get; set; }
    }

    public class PhoneNumberDto
    {
        public string PhoneNumber { get; set; }
        public string DDD { get; set; }
        public string State { get; set; }
    }
}
