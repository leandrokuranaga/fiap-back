using Abp.Domain.Entities;
using Fiap.Domain.GameAggregate;
using Fiap.Domain.LibraryGameAggregate;
using Fiap.Domain.UserAggregate;

namespace Fiap.Domain.LibraryAggregate
{
    public class LibraryDomain : Entity
    {
        public LibraryDomain()
        {
            
        }

        public LibraryDomain(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; set; }
        public virtual UserDomain User { get; set; }

        public virtual ICollection<LibraryGameDomain> Games { get; set; }
    }
}
