using Fiap.Domain.GameAggregate;
using Fiap.Domain.SeedWork;
using Fiap.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;


namespace Fiap.Infra.Data.Repositories
{
    public class GameRepository : BaseRepository<GameDomain>, IGameRepository
    {
        public GameRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

                       
    }
}
