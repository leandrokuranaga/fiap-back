using Fiap.Application.Common;
using Fiap.Application.Games.Models.Requests;
using Fiap.Domain.GameAggregate;
using Fiap.Domain.SeedWork;
using System.Threading.Tasks;
using Fiap.Application.Games.Models.Responses;

namespace Fiap.Application.Games.Services
{
    public class GamesService(INotification notification, IGameRepository gameRepository)
        : BaseService(notification), IGamesService
    {
        private readonly IGameRepository _gameRepository = gameRepository;

        public async Task<BaseResponse<GameResponse>> CreateAsync(CreateGameRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                _notification.AddNotification("Nome", "O nome é obrigatório.", NotificationModel.ENotificationType.BadRequestError);
            }
            if (_notification.HasNotification)
                return BaseResponse<GameResponse>.Fail(_notification.NotificationModel);

            var game = new GameDomain(
                name: request.Name,
                genre: request.Genre,
                price: request.Price,
                promotionId: request.PromotionId
            );

            await _gameRepository.AddAsync(game);

            var response = new GameResponse
            {
                Id = game.Id,
                Name = game.Name,
                Genre = game.Genre,
                Price = game.Price,
                PromotionId = game.PromotionId
            };

            return BaseResponse<GameResponse>.Ok(new GameResponse
            {
                Id = game.Id,
                Name = game.Name,
                Genre = game.Genre,
                Price = game.Price,
                PromotionId = game.PromotionId
            });
        }
    }
}
