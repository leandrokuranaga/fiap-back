using Fiap.Domain.UserAggregate;
using Fiap.Domain.UsersAggregate;
using Fiap.Infra.Data.Repositories.Base;

namespace Fiap.Infra.Data.Repositories
{
    public class UserRepository(IUnitOfWork unitOfWork) : BaseRepository<UserDomain>(unitOfWork), IUserRepository
    {
    }
}
