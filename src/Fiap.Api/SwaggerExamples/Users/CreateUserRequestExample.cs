using Fiap.Application.Users.Models.Request;
using Fiap.Domain.UserAggregate.Enums;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.Api.SwaggerExamples.Users
{
    [ExcludeFromCodeCoverage]
    public class CreateUserAdminRequestExample : IExamplesProvider<CreateUserAdminRequest>
    {
        public CreateUserAdminRequest GetExamples()
        {
            return new CreateUserAdminRequest
            {
                Name = "John Doe",
                Email = "john.doe@hotmail.com",
                Password = "Password123!",
                TypeUser = TypeUser.Admin,
                Active = true
            };
        }
    }
}
