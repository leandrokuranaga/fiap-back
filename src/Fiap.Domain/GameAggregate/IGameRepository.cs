using Fiap.Infra.Data;
using Fiap.Domain.SeedWork;
using Fiap.Domain.Game;


namespace Fiap.Domain.Game
{
    public interface IGameRepository : IBaseRepository<Game>, IUnitOfWork
    {

    }
}
