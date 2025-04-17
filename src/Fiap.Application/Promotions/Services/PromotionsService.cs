using Fiap.Application.Common;
using Fiap.Application.Promotions.Models.Request;
using Fiap.Application.Promotions.Models.Response;
using Fiap.Application.Validators.PromotionsValidators;
using Fiap.Domain.GameAggregate;
using Fiap.Domain.PromotionAggregate;
using Fiap.Domain.SeedWork;
using Fiap.Infra.Data;
using static Fiap.Domain.SeedWork.NotificationModel;

namespace Fiap.Application.Promotions.Services
{
    public class PromotionsService(INotification notification, IPromotionRepository promotionRepository, IGameRepository gameRepository, IUnitOfWork unitOfWork) : BaseService(notification), IPromotionsService
    {
        public Task<PromotionResponse> CreateAsync(CreatePromotionRequest request) => ExecuteAsync(async () =>
        {
            var response = new PromotionResponse();

            try
            {
                Validate(request, new CreatePromotionRequestValidator());

                var promotion = (PromotionDomain)request;

                promotion.ValidatePeriod();

                await unitOfWork.BeginTransactionAsync();

                await promotionRepository.InsertOrUpdateAsync(promotion);
                await promotionRepository.SaveChangesAsync();

                await CreatePromotion(request, promotion);

                await gameRepository.SaveChangesAsync();

                await unitOfWork.CommitAsync();

                response = (PromotionResponse)promotion;

                return response;
            }
            catch (Exception ex)
            {
                notification.AddNotification("Not Found", ex.Message, NotificationModel.ENotificationType.NotFound);
                return response;
            }
        });

        private async Task CreatePromotion(CreatePromotionRequest request, PromotionDomain promotion)
        {

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
                    if (game is null)
                    {
                        _notification.AddNotification($"Game with ID {gameId} Not found", "Not Found", ENotificationType.NotFound);
                        continue; 
                    }

                    game.PromotionId = promotion.Id;
                    games.Add(game);
                    
                }

                if (games.Count != 0)
                {
                    await gameRepository.UpdateRangeAsync(games);
                }
            }
        }

        public Task<PromotionResponse> UpdateAsync(int id, UpdatePromotionRequest request) => ExecuteAsync(async () =>
        {
            var response = new PromotionResponse();

            try
            {
                Validate(request, new UpdatePromotionRequestValidator());

                var promotion = await promotionRepository.GetByIdAsync(id, noTracking: false);

                if (promotion is null)
                {
                    notification.AddNotification("PromotionId", "Promotion not found", NotificationModel.ENotificationType.NotFound);
                    return null!;
                }

                promotion.UpdateDiscount(request.Discount, request.ExpirationDate);
                await unitOfWork.BeginTransactionAsync();

                await promotionRepository.UpdateAsync(promotion);
                await promotionRepository.SaveChangesAsync();

                await UpdateGamesPromotion(request.GameId, promotion.Id);

                await gameRepository.SaveChangesAsync();

                await unitOfWork.CommitAsync();

                response = (PromotionResponse)promotion;

                return response;
            }
            catch (Exception ex)
            {
                notification.AddNotification("Update Promotion", ex.Message, NotificationModel.ENotificationType.NotFound);
                return response;
            }
        });

        private async Task<List<GameDomain>> UpdateGamesPromotion(List<int?>? gameIds, int promotionId)
        {
            var validIds = gameIds?
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .ToList();

            if (validIds == null || validIds.Count == 0)
                return [];

            var games = new List<GameDomain>();

            foreach (var gameId in validIds)
            {
                var game = await gameRepository.GetByIdAsync(gameId, noTracking: false);
                if (game is null)
                {
                    _notification.AddNotification($"Game with ID {gameId} Not found", "Not Found", ENotificationType.NotFound);
                    continue;
                }

                game.AssignPromotion(promotionId);
                games.Add(game);
                
            }

            if (games.Count > 0)
                await gameRepository.UpdateRangeAsync(games);

            return games;
        }

    }
}
