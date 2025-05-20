using Fiap.Domain.GameAggregate;
using Fiap.Infra.Data.Repositories.Base;

namespace Fiap.Infra.Data.Repositories
{
    public class GameRepository(Context context) : BaseRepository<Game>(context), IGameRepository
    {

    }
}
