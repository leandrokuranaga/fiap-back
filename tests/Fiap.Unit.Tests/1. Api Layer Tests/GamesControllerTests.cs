using Fiap.Api.Controllers;
using Fiap.Application.Common;
using Fiap.Application.Games.Models.Request;
using Fiap.Application.Games.Models.Response;
using Fiap.Application.Games.Services;
using Fiap.Application.Users.Models.Response;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Text.Json;

namespace Fiap.Tests._1._Api_Layer_Tests
{
    public class GamesControllerTests
    {
        readonly Mock<IGamesService> _gamesServiceMock;
        readonly Mock<INotification> _notificationMock;
        readonly GamesController _controller;

        public GamesControllerTests()
        {
            _gamesServiceMock = new Mock<IGamesService>();
            _notificationMock = new Mock<INotification>();
            _controller = new GamesController(_gamesServiceMock.Object, _notificationMock.Object);
        }

        #region CreateGame

        [Fact]
        public async Task CreateGame_ShouldReturnOk_WhenServiceReturnsSuccess()
        {
            #region Arrange
            var request = new CreateGameRequest
            {
                Name = "Test Game",
                Genre = "Action",
                Price = 59.99
            };

            var response = new GameResponse
            {
                Id = 1,
                Name = request.Name,
                Genre = request.Genre,
                Price = request.Price,
                PromotionId = null
            };

            _gamesServiceMock
                .Setup(x => x.CreateAsync(request))
                .ReturnsAsync(response);
            #endregion

            #region Act
            var result = await _controller.Create(request);
            #endregion

            #region Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var baseResponse = Assert.IsType<BaseResponse<GameResponse>>(okResult.Value);

            Assert.True(baseResponse.Success);
            Assert.Equal(response.Name, baseResponse.Data.Name);
            #endregion
        }

        #endregion

        #region GetAllGames

        [Fact]
        public async Task GetAllGames_ShouldReturnOk_WhenServiceReturnsGames()
        {
            #region Arrange
            var mockList = new List<GameResponse>
            {
                new GameResponse { Id = 1, Name = "Game 1", Genre = "Action", Price = 10 },
                new GameResponse { Id = 2, Name = "Game 2", Genre = "RPG", Price = 20 }
            };

            _gamesServiceMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(mockList);
            #endregion

            #region Act
            var result = await _controller.GetAll();
            #endregion

            #region Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var baseResponse = Assert.IsType<BaseResponse<IEnumerable<GameResponse>>>(okResult.Value);

            Assert.True(baseResponse.Success);
            Assert.Equal(2, baseResponse.Data.Count());
            #endregion
        }

        #endregion

        [Fact]
        public async Task GetGame_ShouldReturnOk_WhenGameExists()
        {
            // Arrange
            var gameId = 1;
            var response = new GameResponse
            {
                Id = gameId,
                Name = "Zelda",
                Genre = "Adventure",
                Price = 199.90
            };

            _gamesServiceMock
                .Setup(x => x.GetAsync(gameId))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(gameId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var baseResponse = Assert.IsType<BaseResponse<GameResponse>>(okResult.Value);
            Assert.True(baseResponse.Success);
            Assert.Equal(response.Id, baseResponse.Data.Id);
        }

    }
}
