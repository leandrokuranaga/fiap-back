using Fiap.Application.Common;
using Fiap.Application.Games.Models.Request;
using Fiap.Application.Games.Models.Response;
using Fiap.Application.Validators.GamesValidators;
using Fiap.Domain.GameAggregate;
using Fiap.Domain.SeedWork;
using static Fiap.Domain.SeedWork.NotificationModel;

namespace Fiap.Application.Games.Services
{
    public class GamesService(INotification notification, IGameRepository gameRepository) : BaseService(notification), IGamesService
    {
        public async Task<GameResponse> CreateAsync(CreateGameRequest request)
        {
            var response = new GameResponse();

            try
            {
                Validate(request, new CreateGameRequestValidator());

                var name = request.Name.ToLowerInvariant().Trim(); 

                var exists = await gameRepository.ExistAsync(g => g.Name.ToLower() == name);

                if (exists)
                {
                    _notification.AddNotification("Create Game", $"The game '{request.Name}' has already been registered.", ENotificationType.BusinessRules);
                    return response;
                }

                await gameRepository.BeginTransactionAsync();

                var game = (Game)request;
                await gameRepository.InsertOrUpdateAsync(game);

                await gameRepository.SaveChangesAsync();
                await gameRepository.CommitAsync();

                response = (GameResponse)game;
                return response;
            }
            catch (Exception ex)
            {
                await gameRepository.RollbackAsync(); 
                _notification.AddNotification("Create Game", ex.Message, ENotificationType.NotFound);
                return response;
            }
        }

        public Task<IEnumerable<GameResponse>> GetAllAsync() => ExecuteAsync(async () =>
        {
            var games = await gameRepository.GetAllAsync();
            var responses = games.Select(game => (GameResponse)game);
            return responses;
        });
    }
}