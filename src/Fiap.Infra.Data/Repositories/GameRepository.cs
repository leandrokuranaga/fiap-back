using Fiap.Domain.GameAggregate;
using Fiap.Domain.SeedWork;
using Fiap.Infra.Data.Repositories.Base;

namespace Fiap.Infra.Data.Repositories
{
    public class GameRepository : BaseRepository<GameDomain>, IGameRepository
    {
        public GameRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task AddAsync(GameDomain game)
        {
            await InsertOrUpdateAsync(game);
        }
    }
}
