using Fiap.Application.Users.Models.Request;
using Swashbuckle.AspNetCore.Filters;

namespace Fiap.Api.SwaggerExamples.Users
{
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
