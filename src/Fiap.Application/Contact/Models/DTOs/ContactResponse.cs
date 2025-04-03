using System;
using System.Collections.Generic;

namespace Fiap.Application.Contact.Models.DTOs
{
    public class ContactResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EmailResponse> Emails { get; set; }
        public List<PhoneNumberResponse> PhoneNumbers { get; set; }
    }

    public class EmailResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }

    public class PhoneNumberResponse
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string DDD { get; set; }
        public string State { get; set; }
    }
}
