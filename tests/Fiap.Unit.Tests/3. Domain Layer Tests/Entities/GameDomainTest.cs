using Fiap.Domain.GameAggregate;

namespace Fiap.Tests._3._Domain_Layer_Tests
{
    public class GameTest
    {
        [Fact]
        public void GameSuccess()
        {
            #region Arrange
            var mockGame = new Game()
            {
                Id = 1,
                Name = "Test Game",
                Price = 70.0,
                Genre = "Action",
                PromotionId = 1,
            };

            #endregion

            #region Act
            var mockGameAct = new Game(
                mockGame.Id,
                mockGame.Name,
                mockGame.Genre,
                mockGame.Price,
                mockGame.PromotionId
            );
            #endregion

            #region Assert
            Assert.Equal(mockGame.Name, mockGameAct.Name);
            Assert.Equal(mockGame.Genre, mockGameAct.Genre);
            Assert.Equal(mockGame.Price, mockGameAct.Price);
            Assert.Equal(mockGame.PromotionId, mockGameAct.PromotionId);
            #endregion
        }
    }
}
