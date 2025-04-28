using Fiap.Domain.UserAggregate.Enums;

namespace Fiap.Application.Users.Models.Response
{
    public record UserResponse
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public TypeUser Type { get; set; }
        public bool Active { get; set; }

        public static explicit operator UserResponse(Domain.UserAggregate.User user)
        {
            return new UserResponse
            {
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                Type = user.TypeUser,
                Active = user.Active
            };
        }
    }   
}





