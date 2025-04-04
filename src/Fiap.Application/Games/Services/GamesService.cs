using Fiap.Application.Common;
using Fiap.Domain.GameAggregate;
using Fiap.Domain.SeedWork;

namespace Fiap.Application.Games.Services
{
    public class GamesService(INotification notification, IGameRepository gameRepository) : BaseService(notification), IGamesService
    {
    }
}
