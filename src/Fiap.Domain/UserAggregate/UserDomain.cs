using Abp.Domain.Entities;
using Fiap.Domain.LibraryAggregate;
using Fiap.Domain.SeedWork.Enums;

namespace Fiap.Domain.UserAggregate
{
    public class UserDomain : Entity
    {
        public UserDomain()
        {
            
        }

        public UserDomain(string name, string email, string password, TypeUser typeUser, bool active)
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
        public virtual LibraryDomain Library { get; set; }

        public static UserDomain CreateByAdmin(string name, string email, string password, TypeUser typeUser, bool active)
        {
            return new UserDomain(name, email, password, typeUser, active);
        }

        public static UserDomain CreateByPublic(string name, string email, string password)
        {
            return new UserDomain(name, email, password, TypeUser.User, true);
        }
    }
}
