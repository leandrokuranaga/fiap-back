using Fiap.Domain.SeedWork.Enums;

namespace Fiap.Application.Users.Models.Request
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public TypeUser Type { get; set; }
        public bool Active { get; set; }
    }
}
