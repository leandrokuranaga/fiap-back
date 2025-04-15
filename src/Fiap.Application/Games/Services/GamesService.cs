using Fiap.Application.Common;
using Fiap.Application.Games.Models.Request;
using Fiap.Application.Games.Models.Response;
using Fiap.Application.Validators.GamesValidators;
using Fiap.Domain.GameAggregate;
using Fiap.Domain.SeedWork;
using static Fiap.Domain.SeedWork.NotificationModel;

namespace Fiap.Application.Games.Services
{
    public class GamesService(INotification notification, IGameRepository gameRepository)
        : BaseService(notification), IGamesService
    {
        public Task<GameResponse> CreateAsync(CreateGameRequest request) => ExecuteAsync(async () =>
        {
            var response = new GameResponse();

            try
            {
                Validate(request, new CreateGameRequestValidator());

                var game = (GameDomain)request;
                await gameRepository.InsertOrUpdateAsync(game);

                response = (GameResponse)game;
                return response;
            }
            catch (Exception ex)
            {
                notification.AddNotification("Create Game", ex.Message, ENotificationType.NotFound);
                return response;
            }
        });

        public Task<IEnumerable<GameResponse>> GetAllAsync() => ExecuteAsync(async () =>
        {
            var games = await gameRepository.GetAllAsync();
            var responses = games.Select(game => (GameResponse)game);
            return responses;
        });
    }
}
