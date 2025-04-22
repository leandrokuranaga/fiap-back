using Fiap.Domain.GameAggregate;
using Fiap.Domain.SeedWork;
using Fiap.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Infra.Data.Repositories
{
    public class GameRepository(IUnitOfWork unitOfWork)
        : BaseRepository<GameDomain>(unitOfWork), IGameRepository
    {
        public async Task<GameDomain?> GetByNameAsync(string name)
        {
            return await GetOneNoTracking(game =>
                EF.Property<string>(game, "Name").ToLower() == name.ToLower());
        }

    }
}
