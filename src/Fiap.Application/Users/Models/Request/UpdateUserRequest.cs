using Fiap.Application.Common;
using Fiap.Domain.UserAggregate;
using Fiap.Domain.UserAggregate.Enums;

namespace Fiap.Application.Users.Models.Request
{
    public record UpdateUserRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public TypeUser? Type { get; set; }
        public bool? Active { get; set; }
    }
}
