using Fiap.Application.Common;
using Fiap.Domain.SeedWork.Enums;
using Fiap.Domain.UserAggregate;

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
