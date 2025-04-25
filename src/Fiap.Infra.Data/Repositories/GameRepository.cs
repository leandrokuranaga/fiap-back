using Fiap.Domain.GameAggregate;
using Fiap.Domain.SeedWork;
using Fiap.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Infra.Data.Repositories
{
    public class GameRepository(IUnitOfWork unitOfWork)
        : BaseRepository<GameDomain>(unitOfWork), IGameRepository
    {

    }
}
