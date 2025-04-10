using Fiap.Application.Common;
using Fiap.Domain.UserAggregate;

namespace Fiap.Application.Users.Models.Request
{
    public record CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static explicit operator UserDomain(CreateUserRequest request)
        {
            return UserDomain.CreateByPublic(
                request.Name,
                request.Email,
                PasswordHasher.HashPassword(request.Password)
            );
        }
    }
}
