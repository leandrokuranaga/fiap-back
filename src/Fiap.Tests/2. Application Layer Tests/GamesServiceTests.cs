using Fiap.Application.Games.Models.Request;
using Fiap.Application.Games.Services;
using Fiap.Domain.GameAggregate;
using Fiap.Domain.SeedWork;
using Moq;

namespace Fiap.Tests._2._Application_Layer_Tests
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
                Price = 99.99
            };

            _mockGameRepository
                .Setup(repo => repo.InsertOrUpdateAsync(It.IsAny<GameDomain>()))
                .ReturnsAsync((GameDomain game) =>
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
                Price = 59.99
            };

            _mockGameRepository
                .Setup(repo => repo.InsertOrUpdateAsync(It.IsAny<GameDomain>()))
                .ThrowsAsync(new Exception("Test exception"));
            #endregion

            #region Act
            var result = await _gameService.CreateAsync(request);
            #endregion

            #region Assert
            Assert.NotNull(result);
            _mockNotification.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NotificationModel.ENotificationType>()), Times.Once);
            #endregion
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnGames_WhenGamesExist()
        {
            #region Arrange
            var games = new List<GameDomain>
            {
                new GameDomain("Game 1", "Ação", 59.90, null) { Id = 1 },
                new GameDomain("Game 2", "Aventura", 49.90, null) { Id = 2 }
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
            var emptyGames = new List<GameDomain>();

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


    }
}
