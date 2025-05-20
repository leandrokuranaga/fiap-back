using System.Diagnostics.CodeAnalysis;

namespace Fiap.Application.Auth.Models.Request
{
    [ExcludeFromCodeCoverage]
    public record LoginRequest
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
