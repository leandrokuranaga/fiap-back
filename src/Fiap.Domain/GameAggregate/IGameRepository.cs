using Fiap.Domain.SeedWork;

namespace Fiap.Domain.GameAggregate
{
    public interface IGameRepository : IBaseRepository<GameDomain>
    {
        Task AddAsync(GameDomain game);
    }
}
