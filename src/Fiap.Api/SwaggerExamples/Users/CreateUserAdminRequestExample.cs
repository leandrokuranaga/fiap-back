using Fiap.Application.Users.Models.Request;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Api.SwaggerExamples.Users
{
    [ExcludeFromCodeCoverage]
    public class CreateUserRequestExample : IExamplesProvider<CreateUserRequest>
    {
        public CreateUserRequest GetExamples()
        {
            return new CreateUserRequest
            {
                Name = "John Doe",
                Email = "john.doe@hotmail.com",
                Password = "Password123!"
            };
        }
    }
}
