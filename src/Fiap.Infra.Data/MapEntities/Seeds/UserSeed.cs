using Fiap.Domain.UserAggregate;
using Fiap.Domain.UserAggregate.Enums;
using Fiap.Domain.UserAggregate.ValueObjects;

namespace Fiap.Infra.Data.MapEntities.Seeds
{
    public static class UserSeed
    {
        public static List<User> Users()
        {
            return
            [
                new() {
                    Id = 1,
                    Name = "Admin",
                 //   Email = new Email("admin@gmail.com"),
                    TypeUser = TypeUser.Admin,
                    Active = true
                },
                new() {
                    Id = 2,
                    Name = "User",
                 //   Email = new Email("user@gmail.com"),
                    TypeUser = TypeUser.User,
                    Active = true
                }
            ];
        }
    }
}
