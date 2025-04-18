using Fiap.Domain.UserAggregate;
using Fiap.Domain.UserAggregate.Enums;

namespace Fiap.Infra.Data.MapEntities.Seeds
{
    public static class UserSeed
    {
        public static List<User> Users()
        {
            return [
                new User
                {
                    Id = 1,
                    Name = "Admin",
                    Email = "admin@gmail.com",
                    Password = "$2a$11$GtOwXg2TwrUQJZJP0rfbDO93ZdUuDAE6RrfI8sFSa5Zq1/hXQ6CKq",
                    TypeUser = TypeUser.Admin,
                    Active = true
                },
                new User
                {
                    Id = 2,
                    Name = "User",
                    Email = "user@gmail.com",
                    Password = "$2a$11$GtOwXg2TwrUQJZJP0rfbDO93ZdUuDAE6RrfI8sFSa5Zq1/hXQ6CKq",
                    TypeUser = TypeUser.User,
                    Active = true
                }
            ];
        }
    }
}
