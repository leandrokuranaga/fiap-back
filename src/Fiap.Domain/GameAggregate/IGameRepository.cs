using Fiap.Domain.SeedWork;

namespace Fiap.Domain.GameAggregate
{
    public interface IGameRepository : IBaseRepository<Game>, IUnitOfWork
    {

    }
}
