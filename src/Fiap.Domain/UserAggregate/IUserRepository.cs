using Fiap.Domain.SeedWork;
using Fiap.Domain.UserAggregate;
using Fiap.Infra.Data;

namespace Fiap.Domain.UsersAggregate
{
    public interface IUserRepository : IBaseRepository<User>, IUnitOfWork
    {
    }
}
