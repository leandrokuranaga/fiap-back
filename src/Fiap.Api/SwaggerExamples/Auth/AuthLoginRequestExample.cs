using Fiap.Application.Auth.Models.Request;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Api.SwaggerExamples.Auth
{
    [ExcludeFromCodeCoverage]
    public class AuthLoginRequestExample : IExamplesProvider<LoginRequest>
    {
        public LoginRequest GetExamples()
        {
            return new LoginRequest
            {
                Username = "john.doe@hotmail.com",
                Password = "Password123!"
            };
        }
    }
}
