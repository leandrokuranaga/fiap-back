using Fiap.Application.Users.Models.Request;
using Fiap.Domain.UserAggregate.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace Fiap.Api.SwaggerExamples.Users
{
    public class UpdateUserRequestExample : IExamplesProvider<UpdateUserRequest>
    {
        public UpdateUserRequest GetExamples()
        {
            return new UpdateUserRequest
            {
                Name = "Maria Carie",
                Email = "maria.carie@hotmail.com",
                Password = "Password456!",
                Type = TypeUser.Admin,
                Active = false
            };
        }
    }
}
