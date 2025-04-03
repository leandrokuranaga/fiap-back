using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Application.Contact.Models.Request
{
    public class UpdateContactRequest
    {
        public int Id { get; set; } // ou int, dependendo da sua chave
        public string Name { get; set; }
        public List<UpdateEmailDto> Emails { get; set; }
        public List<UpdatePhoneNumberDto> PhoneNumbers { get; set; }
    }

    public class UpdateEmailDto
    {
        public int? Id { get; set; } // Se for atualizar ou deletar, precisa do ID
        public string Email { get; set; }
    }

    public class UpdatePhoneNumberDto
    {
        public int? Id { get; set; }
        public string PhoneNumber { get; set; }
        public string DDD { get; set; }
        public string State { get; set; }
    }
}
