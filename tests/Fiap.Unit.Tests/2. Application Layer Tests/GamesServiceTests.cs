using Fiap.Application.Games.Models.Request;
using Fiap.Application.Games.Services;
using Fiap.Domain.GameAggregate;
using Fiap.Domain.SeedWork;
using System.Linq.Expressions;
using Moq;
using Fiap.Domain.SeedWork.Exceptions;

namespace Fiap.Unit.Tests.Application_Layer_Tests
{
    public class GamesServiceTests
    {
        private readonly Mock<IGameRepository> _mockGameRepository;
        private readonly Mock<INotification> _mockNotification;
        private readonly GamesService _gameService;

        public GamesServiceTests()
        {
            _mockGameRepository = new Mock<IGameRepository>();
            _mockNotification = new Mock<INotification>();
            _gameService = new GamesService(_mockNotification.Object, _mockGameRepository.Object);
        }

        [Fact]
        public async Task CreateGame_ShouldReturnGameId_WhenValidRequest()
        {
            #region Arrange
            var request = new CreateGameRequest
            {
                Name = "Test Game",
                Genre = "Action",
                Price = 99.99M
            };

            _mockGameRepository
                .Setup(repo => repo.InsertOrUpdateAsync(It.IsAny<Game>()))
                .ReturnsAsync((Game game) =>
                {
                    game.Id = 1;
                    return game;
                });
            #endregion

            #region Act
            var result = await _gameService.CreateAsync(request);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.Genre, result.Genre);
            Assert.Equal(request.Price, result.Price);
            #endregion
        }

        [Fact]
        public async Task CreateGame_ShouldAddNotification_WhenExceptionOccurs()
        {
            #region Arrange
            var request = new CreateGameRequest
            {
                Name = "Error Game",
                Genre = "Adventure",
                Price = 59.99M
            };

            _mockGameRepository
                .Setup(repo => repo.InsertOrUpdateAsync(It.IsAny<Game>()))
                .ThrowsAsync(new Exception("Test exception"));
            #endregion

            #region Act
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _gameService.CreateAsync(request));
            #endregion

            #region Assert
            Assert.NotNull(exception);
            Assert.Equal("Test exception", exception.Message);

            _mockNotification.Verify(
                n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NotificationModel.ENotificationType>()),
                Times.Once
            );
            #endregion
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnGames_WhenGamesExist()
        {
            #region Arrange
            var games = new List<Game>
            {
                new Game("Game 1", "Action", 59.90M, null) { Id = 1 },
                new Game("Game 2", "Adventure", 49.90M, null) { Id = 2 }
            };

            _mockGameRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(games);
            #endregion

            #region Act
            var result = await _gameService.GetAllAsync();
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, g => g.Id == 1 && g.Name == "Game 1");
            Assert.Contains(result, g => g.Id == 2 && g.Name == "Game 2");
            #endregion
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoGamesExist()
        {
            #region Arrange
            var emptyGames = new List<Game>();

            _mockGameRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(emptyGames);
            #endregion

            #region Act
            var result = await _gameService.GetAllAsync();
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            #endregion
        }

        [Fact]
        public async Task CreateGame_ShouldAddNotification_WhenGameAlreadyExists()
        {
            #region Arrange
            var request = new CreateGameRequest
            {
                Name = "Existing Game",
                Genre = "Action",
                Price = 49.99M
            };

            _mockGameRepository
                .Setup(repo => repo.ExistAsync(It.IsAny<Expression<Func<Game, bool>>>()))
                .ReturnsAsync(true);
            #endregion

            #region Act
            var result = await _gameService.CreateAsync(request);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Id);
            _mockNotification.Verify(
                n => n.AddNotification(
                    "Create Game",
                    $"The game '{request.Name}' has already been registered.",
                    NotificationModel.ENotificationType.BusinessRules),
                Times.Once
            );
            #endregion
        }

        [Fact]
        public async Task CreateGame_ShouldThrowException_WhenCurrencyIsInvalid()
        {
            #region Arrange
            var request = new CreateGameRequest
            {
                Name = "Invalid Currency Game",
                Genre = "Action",
                Price = 49.99M
            };

            _mockGameRepository
                .Setup(repo => repo.InsertOrUpdateAsync(It.IsAny<Game>()))
                .ThrowsAsync(new BusinessRulesException("Invalid currency: INVALID. Supported currencies are: USD, EUR, BRL, JPY, GBP"));

            #endregion
        }

        [Fact]
        public async Task GetGameAsync_ShouldReturnGame_WhenGameExists()
        {
            // Arrange
            var gameId = 1;
            var game = new Game("Halo", "Shooter", 199.99M, null) { Id = gameId };

            _mockGameRepository
                .Setup(repo => repo.GetByIdAsync(gameId, false))
                .ReturnsAsync(game);

            // Act
            var result = await _gameService.GetAsync(gameId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(gameId, result.Id);
            Assert.Equal("Halo", result.Name);
            Assert.Equal("Shooter", result.Genre);
            Assert.Equal(199.99M, result.Price);
        }


        [Fact]
        public async Task GetGameAsync_ShouldAddNotification_WhenGameDoesNotExist()
        {
            // Arrange
            var gameId = 99;

            _mockGameRepository
                .Setup(repo => repo.GetByIdAsync(gameId, false))
                .ReturnsAsync((Game?)null);

            // Act
            var result = await _gameService.GetAsync(gameId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Id);
            _mockNotification.Verify(
                n => n.AddNotification(
                    "Get game by id",
                    $"Game not found with id {gameId}",
                    NotificationModel.ENotificationType.NotFound),
                Times.Once
            );
        }

    }
}
