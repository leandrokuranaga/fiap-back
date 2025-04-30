using System.Diagnostics.CodeAnalysis;

namespace Fiap.Application.Auth.Models.Response
{
    [ExcludeFromCodeCoverage]
    public record LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
