using Fiap.Domain.UserAggregate;
using Fiap.Domain.UserAggregate.Enums;

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
                    Email = "admin@gmail.com",
                    TypeUser = TypeUser.Admin,
                    Active = true
                },
                new() {
                    Id = 2,
                    Name = "User",
                    Email = "user@gmail.com",
                    TypeUser = TypeUser.User,
                    Active = true
                }
            ];
        }
    }
}
