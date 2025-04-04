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
            Email = email;
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
    }
}
