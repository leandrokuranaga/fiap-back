using Fiap.Domain.UserAggregate.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Application.Users.Models.Request
{
    [ExcludeFromCodeCoverage]
    public record UpdateUserRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public TypeUser? Type { get; set; }
        public bool? Active { get; set; }
    }
}
