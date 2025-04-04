using Fiap.Domain.GameAggregate;
using Fiap.Infra.Data.Repositories.Base;

namespace Fiap.Infra.Data.Repositories
{
    public class GameRepository(IUnitOfWork unitOfWork) : BaseRepository<GameDomain>(unitOfWork), IGameRepository
    {
    }
}
