using Fiap.Domain.Game;
using Fiap.Domain.SeedWork;
using Fiap.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Infra.Data.Repositories
{
    public class GameRepository(Context context) : BaseRepository<Game>(context), IGameRepository
    {

    }
}
