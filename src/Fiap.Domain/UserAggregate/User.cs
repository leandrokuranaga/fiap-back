using Abp.Domain.Entities;
using Fiap.Domain.UserAggregate.Entities;
using Fiap.Domain.UserAggregate.Enums;

namespace Fiap.Domain.UserAggregate
{
    public class User : Entity
    {
        public User()
        {
            
        }

        public User(string name, string email, string password, TypeUser typeUser, bool active)
        {
            Name = name;
            Email = email.Trim().ToLowerInvariant();
            Password = password;
            TypeUser = typeUser;
            Active = active;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public TypeUser TypeUser { get; set; }
        public bool Active { get; set; }
        public ICollection<LibraryGame> LibraryGames { get; private set; } = [];

        public static User CreateByAdmin(string name, string email, string password, TypeUser typeUser, bool active)
        {
            return new User(name, email, password, typeUser, active);
        }

        public static User CreateByPublic(string name, string email, string password)
        {
            return new User(name, email, password, TypeUser.User, true);
        }
    }
}
