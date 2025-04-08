using Fiap.Domain.SeedWork.Enums;

namespace Fiap.Application.Users.Models.Response
{
    public class GetUserResponse
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public TypeUser Type { get; set; }
        public bool Active { get; set; }
    }
}





