using Fiap.Application.Common;
using Fiap.Application.Games.Models.Request;
using Fiap.Application.Games.Models.Response;
using Fiap.Application.Validators.GamesValidators;
using Fiap.Domain.GameAggregate;
using Fiap.Domain.SeedWork;
using static Fiap.Domain.SeedWork.NotificationModel;

namespace Fiap.Application.Games.Services
{
    public class GamesService : BaseService, IGamesService
    {
        private readonly IGameRepository _gameRepository;

        public GamesService(INotification notification, IGameRepository gameRepository)
            : base(notification)
        {
            _gameRepository = gameRepository;
        }

        public async Task<GameResponse> CreateAsync(CreateGameRequest request)
        {
            var response = new GameResponse();

            try
            {
                Validate(request, new CreateGameRequestValidator());

                var exists = await _gameRepository.ExistAsync(g => g.Name.ToLower() == request.Name.ToLower());

                if (exists)
                {
                    _notification.AddNotification("Create Game", $"The game '{request.Name}' has already been registered.", ENotificationType.BusinessRules);
                    return response;
                }

                var game = (GameDomain)request;
                await _gameRepository.InsertOrUpdateAsync(game);

                response = (GameResponse)game;
                return response;
            }
            catch (Exception ex)
            {
                _notification.AddNotification("Create Game", ex.Message, ENotificationType.NotFound);
                return response;
            }
        }

        public Task<IEnumerable<GameResponse>> GetAllAsync() => ExecuteAsync(async () =>
        {
            var games = await _gameRepository.GetAllAsync();
            var responses = games.Select(game => (GameResponse)game);
            return responses;
        });
    }
}