using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Application.Auth.Models
{
    public record LoginRequest
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
