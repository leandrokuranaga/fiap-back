using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Application.Auth.Models
{
    public record LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool Success { get; init; }
    }
}
