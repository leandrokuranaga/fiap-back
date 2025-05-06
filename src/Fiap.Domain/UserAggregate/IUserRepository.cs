using Fiap.Domain.SeedWork;

namespace Fiap.Domain.UserAggregate
{
    public interface IUserRepository : IBaseRepository<User>, IUnitOfWork
    {
        Task<User> GetByIdGameUserAsync(int id, bool noTracking);
    }
}
