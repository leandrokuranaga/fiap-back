using Fiap.Infra.Data;
using Fiap.Domain.SeedWork;

namespace Fiap.Domain.Game
{
    public interface IGameRepository : IBaseRepository<Game>, IUnitOfWork
    {
    }
}
