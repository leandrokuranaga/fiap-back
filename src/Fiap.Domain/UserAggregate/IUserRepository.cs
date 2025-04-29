using Fiap.Domain.SeedWork;

namespace Fiap.Domain.UserAggregate
{
    public interface IUserRepository : IBaseRepository<User>, IUnitOfWork
    {
    }
}
