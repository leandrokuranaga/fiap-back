using Fiap.Application.Promotions.Models.Request;
using Fiap.Application.Promotions.Services;
using Fiap.Domain.GameAggregate;
using Fiap.Domain.GameAggregate.ValueObjects;
using Fiap.Domain.PromotionAggregate;
using Fiap.Domain.SeedWork;
using Moq;
using static Fiap.Domain.SeedWork.NotificationModel;

namespace Fiap.Tests._2._Application_Layer_Tests
{
    public class PromotionServiceTests
    {
        readonly Mock<IPromotionRepository> _mockPromotionRepositoryMock;
        readonly Mock<IGameRepository> _mockGameRepositoryMock;
        readonly Mock<INotification> _mockNotification;
        readonly Mock<IUnitOfWork> _mockUnitOfWork;
        readonly PromotionsService _promotionService;

        public PromotionServiceTests()
        {
            _mockPromotionRepositoryMock = new Mock<IPromotionRepository>();
            _mockGameRepositoryMock = new Mock<IGameRepository>();
            _mockNotification = new Mock<INotification>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _promotionService = new PromotionsService(_mockNotification.Object, _mockPromotionRepositoryMock.Object, _mockGameRepositoryMock.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task CreatePromotion_ShouldReturnPromotionId_WhenValidRequest()
        {
            #region Arrange
            var now = DateTime.UtcNow;

            var request = new CreatePromotionRequest
            {
                Discount = 10,
                ExpirationDate = now.AddDays(30),
                GameId = [1, 2, 3]
            };

            _mockPromotionRepositoryMock
                .Setup(repo => repo.InsertOrUpdateAsync(It.IsAny<Promotion>()))
                .ReturnsAsync((Promotion p) => 
                {
                    p.Id = 1;
                    return p;
                });

            _mockGameRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(new Game());
            #endregion

            #region Act
            var result = await _promotionService.CreateAsync(request);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.True(result.PromotionId > 0);
            Assert.Equal(request.Discount, result.Discount);
            Assert.Equal(request.ExpirationDate, result.EndDate);
            Assert.True(
                (DateTime.UtcNow - result.StartDate).TotalSeconds < 5,
                $"StartDate was too far off from expected time: {result.StartDate}"
            );
            #endregion
        }


        [Fact]
        public async Task CreatePromotion_ShouldAddNotification_WhenExceptionOccurs()
        {
            #region Arrange
            var request = new CreatePromotionRequest
            {
                Discount = 10,
                ExpirationDate = DateTime.UtcNow.AddDays(30),
                GameId = new List<int?> { 1, 2, 3 }
            };
            _mockPromotionRepositoryMock.Setup(repo => repo.InsertOrUpdateAsync(It.IsAny<Promotion>()))
                .ThrowsAsync(new Exception("Test exception"));

            #endregion

            #region Act
            var result = await _promotionService.CreateAsync(request);
            #endregion

            #region Assert
            Assert.NotNull(result);
            _mockNotification.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NotificationModel.ENotificationType>()), Times.Once);
            #endregion
        }

        [Fact]
        public async Task UpdatePromotion_ShouldReturnTrue_WhenValidRequest()
        {

            #region Arrange

            int promotionId = 1;
            var request = new UpdatePromotionRequest
            {
                Discount = 10,
                ExpirationDate = DateTime.UtcNow.AddDays(30)
            };

            var promotion = new Promotion(request.Discount ?? 0, DateTime.UtcNow, request.ExpirationDate ?? DateTime.UtcNow)
            {
                Id = promotionId
            };

            _mockPromotionRepositoryMock.Setup(repo => repo.GetByIdAsync(promotionId, It.IsAny<bool>()))
                .ReturnsAsync(promotion);

            _mockPromotionRepositoryMock.Setup(repo => repo.InsertOrUpdateAsync(It.IsAny<Promotion>()))
                .ReturnsAsync(promotion);
            #endregion

            #region Act
            var result = await _promotionService.UpdateAsync(promotionId, request);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(promotionId, result.PromotionId);
            Assert.Equal(request.Discount, result.Discount);
            Assert.Equal(request.ExpirationDate, result.EndDate);
            Assert.Equal(promotion.StartDate, result.StartDate);

            #endregion
        }

        [Fact]
        public async Task UpdatePromotion_ShouldAddNotification_WhenExceptionOccurs()
        {
            #region Arrange
            int promotionId = 1;

            var request = new UpdatePromotionRequest
            {
                Discount = 10,
                ExpirationDate = DateTime.UtcNow.AddDays(30)
            };

            _mockPromotionRepositoryMock.Setup(repo => repo.GetByIdAsync(promotionId, It.IsAny<bool>()))
                .ThrowsAsync(new Exception("Test exception"));

            #endregion

            #region Act
            var result = await _promotionService.UpdateAsync(promotionId, request);

            #endregion

            #region Assert
            Assert.NotNull(result);
            _mockNotification.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NotificationModel.ENotificationType>()), Times.Once);

            #endregion
        }

        [Fact]
        public async Task UpdatePromotion_ShouldUpdateGames_WhenGameIdsAreProvided()
        {
            #region Arrange
            int promotionId = 1;

            var request = new UpdatePromotionRequest
            {
                Discount = 15,
                ExpirationDate = DateTime.UtcNow.AddDays(20),
                GameId = [101, 102]
            };

            var promotion = new Promotion(request.Discount.Value, DateTime.UtcNow, request.ExpirationDate.Value)
            {
                Id = promotionId
            };

            var game1 = new Game() { Id = 101, Name = "Game 1", Genre = new Genre("Action"), Price = 59.90 };
            var game2 = new Game() { Id = 102, Name = "Game 2", Genre = new Genre("Adventure"), Price = 49.90 };

            _mockPromotionRepositoryMock.Setup(repo => repo.GetByIdAsync(promotionId, It.IsAny<bool>()))
                .ReturnsAsync(promotion);

            _mockGameRepositoryMock.Setup(repo => repo.GetByIdAsync(101, It.IsAny<bool>()))
                .ReturnsAsync(game1);

            _mockGameRepositoryMock.Setup(repo => repo.GetByIdAsync(102, It.IsAny<bool>()))
                .ReturnsAsync(game2);

            _mockPromotionRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Promotion>()))
                .Returns(Task.CompletedTask);

            _mockGameRepositoryMock.Setup(repo => repo.UpdateRangeAsync(It.IsAny<IEnumerable<Game>>()))
                .Returns(Task.CompletedTask);

            #endregion

            #region Act
            var result = await _promotionService.UpdateAsync(promotionId, request);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(promotionId, result.PromotionId);
            Assert.Equal(request.Discount, result.Discount);
            Assert.Equal(request.ExpirationDate, result.EndDate);
            Assert.Equal(promotion.StartDate, result.StartDate);

            _mockGameRepositoryMock.Verify(repo => repo.UpdateRangeAsync(It.Is<IEnumerable<Game>>(games =>
                games.Any(g => g.Id == 101 && g.PromotionId == promotionId) &&
                games.Any(g => g.Id == 102 && g.PromotionId == promotionId) 
            )), Times.Once);
            #endregion
        }

        [Fact]
        public async Task UpdatePromotion_ShouldAddNotification_WhenPromotionNotFound()
        {
            #region Arrange
            var promotionId = 1;
            var request = new UpdatePromotionRequest
            {
                Discount = 15,
                ExpirationDate = DateTime.UtcNow.AddDays(10)
            };

            _mockPromotionRepositoryMock
                .Setup(repo => repo.GetByIdAsync(promotionId, It.IsAny<bool>()))
                .ReturnsAsync((Promotion?)null);
            #endregion

            #region Act
            var result = await _promotionService.UpdateAsync(promotionId, request);
            #endregion

            #region Assert
            Assert.Null(result);
            _mockNotification.Verify(n =>
                n.AddNotification("PromotionId", "Promotion not found", NotificationModel.ENotificationType.NotFound),
                Times.Once);
            #endregion
        }

        [Fact]
        public async Task UpdatePromotion_ShouldAddNotification_WhenSomeGamesNotFound()
        {
            #region Arrange
            int promotionId = 1;

            var request = new UpdatePromotionRequest
            {
                Discount = 15,
                ExpirationDate = DateTime.UtcNow.AddDays(20),
                GameId = [101, 102, 999]
            };

            var promotion = new Promotion(request.Discount.Value, DateTime.UtcNow, request.ExpirationDate.Value)
            {
                Id = promotionId
            };

            var game1 = new Game() { Id = 101, Name = "Game 1", Genre = new Genre("Action"), Price = 59.90 };
            var game2 = new Game() { Id = 102, Name = "Game 2", Genre = new Genre("Adventure"), Price = 49.90 };

            _mockPromotionRepositoryMock
                .Setup(repo => repo.GetByIdAsync(promotionId, It.IsAny<bool>()))
                .ReturnsAsync(promotion);

            _mockGameRepositoryMock
                .Setup(repo => repo.GetByIdAsync(101, It.IsAny<bool>()))
                .ReturnsAsync(game1);

            _mockGameRepositoryMock
                .Setup(repo => repo.GetByIdAsync(102, It.IsAny<bool>()))
                .ReturnsAsync(game2);

            _mockGameRepositoryMock
                .Setup(repo => repo.GetByIdAsync(999, It.IsAny<bool>()))
                .ReturnsAsync((Game?)null);

            _mockPromotionRepositoryMock
                .Setup(repo => repo.UpdateAsync(It.IsAny<Promotion>()))
                .Returns(Task.CompletedTask);

            _mockGameRepositoryMock
                .Setup(repo => repo.UpdateRangeAsync(It.IsAny<IEnumerable<Game>>()))
                .Returns(Task.CompletedTask);

            #endregion

            #region Act
            var result = await _promotionService.UpdateAsync(promotionId, request);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(promotionId, result.PromotionId);
            Assert.Equal(request.Discount, result.Discount);
            Assert.Equal(request.ExpirationDate, result.EndDate);

            _mockNotification.Verify(n =>
                n.AddNotification("Game with ID 999 Not found", "Not Found", ENotificationType.NotFound),
                Times.Once);
            #endregion
        }

        [Fact]
        public async Task CreatePromotion_ShouldAddNotification_WhenSomeGamesNotFound()
        {
            #region Arrange
            var request = new CreatePromotionRequest
            {
                Discount = 20,
                ExpirationDate = DateTime.UtcNow.AddDays(10),
                GameId = [1, 2, 3]
            };

            var promotion = new Promotion(request.Discount, DateTime.UtcNow, request.ExpirationDate)
            {
                Id = 99
            };

            _mockPromotionRepositoryMock
                .Setup(repo => repo.InsertOrUpdateAsync(It.IsAny<Promotion>()))
                .ReturnsAsync((Promotion p) =>
                {
                    p.Id = 99; 
                    return p;
                });

            _mockGameRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync((Game?)null);

            _mockUnitOfWork.Setup(uow => uow.BeginTransactionAsync()).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync()).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(uow => uow.CommitAsync()).Returns(Task.CompletedTask);
            #endregion

            #region Act
            var result = await _promotionService.CreateAsync(request);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(99, result.PromotionId);
            _mockNotification.Verify(n =>
                n.AddNotification(It.Is<string>(s => s.Contains("Game with ID")), "Not Found", NotificationModel.ENotificationType.NotFound),
                Times.Exactly(3));
            #endregion
        }

    }
}
