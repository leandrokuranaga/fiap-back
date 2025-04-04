using Fiap.Application.Common;
using Fiap.Application.Promotions.Models.Request;
using Fiap.Application.Promotions.Models.Response;
using Fiap.Application.Validators.PromotionsValidators;
using Fiap.Domain.GameAggregate;
using Fiap.Domain.PromotionAggregate;
using Fiap.Domain.SeedWork;

namespace Fiap.Application.Promotions.Services
{
    public class PromotionsService(INotification notification, IPromotionRepository promotionRepository, IGameRepository gameRepository) : BaseService(notification), IPromotionsService
    {
        public async Task<CreatePromotionResponse> CreateAsync(CreatePromotionRequest request)
        {
            var response = new CreatePromotionResponse();

            try
            {
                Validate(request, new CreatePromotionRequestValidator());

                var promotion = new PromotionDomain(
                    request.Discount,
                    DateTime.UtcNow,
                    request.ExpirationDate
                );

                await promotionRepository.InsertOrUpdateAsync(promotion);

                if (request.GameId != null && request.GameId.Count != 0)
                {
                    var validIds = request.GameId
                        .Where(id => id.HasValue)
                        .Select(id => id.Value)
                        .ToList();

                    var games = new List<GameDomain>();

                    foreach (var gameId in validIds)
                    {
                        var game = await gameRepository.GetByIdAsync(gameId, noTracking: false);
                        if (game is not null)
                        {
                            game.PromotionId = promotion.Id;
                            games.Add(game);
                        }
                    }

                    if (games.Count != 0)
                    {
                        await gameRepository.UpdateRangeAsync(games);
                    }
                }

                response.PromotionId = promotion.Id;
                return response;
            }
            catch (Exception ex)
            {
                notification.AddNotification("Not Found", ex.Message, NotificationModel.ENotificationType.NotFound);
                return response;
            }
        }


        public async Task<UpdatePromotionResponse> UpdateAsync(UpdatePromotionRequest request)
        {
            var response = new UpdatePromotionResponse();

            try
            {
                Validate(request, new UpdatePromotionRequestValidator());

                var promotion = await promotionRepository.GetByIdAsync(request.Id, noTracking: false);

                if (request.Discount.HasValue)
                    promotion.Discount = request.Discount.Value;

                if (request.ExpirationDate.HasValue)
                    promotion.EndDate = request.ExpirationDate.Value;

                await promotionRepository.UpdateAsync(promotion);

                if (request.GameId != null && request.GameId.Count != 0)
                {
                    var validIds = request.GameId
                        .Where(id => id.HasValue)
                        .Select(id => id.Value)
                        .ToList();

                    var games = new List<GameDomain>();

                    foreach (var gameId in validIds)
                    {
                        var game = await gameRepository.GetByIdAsync(gameId, noTracking: false);
                        if (game is not null)
                        {
                            game.PromotionId = promotion.Id;
                            games.Add(game);
                        }
                    }

                    if (games.Count != 0)
                        await gameRepository.UpdateRangeAsync(games);
                }

                response.PromotionId = promotion.Id;
                return response;
            }
            catch (Exception ex)
            {
                notification.AddNotification("Update Promotion", ex.Message, NotificationModel.ENotificationType.NotFound);
                return response;
            }
        }

    }
}
