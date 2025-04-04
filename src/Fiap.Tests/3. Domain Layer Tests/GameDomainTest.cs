using Fiap.Domain.GameAggregate;

namespace Fiap.Tests._3._Domain_Layer_Tests
{
    public class GameDomainTest
    {
        [Fact]
        public void GameDomainSuccess()
        {
            #region Arrange
            var mockGameDomain = new GameDomain()
            {
                Name = "Test Game",
                Price = 70.0,
                Genre = "Action",
                PromotionId = 1,
            };

            #endregion

            #region Act
            var mockGameDomainAct = new GameDomain(
                mockGameDomain.Name,
                mockGameDomain.Genre,
                mockGameDomain.Price,
                mockGameDomain.PromotionId
            );
            #endregion

            #region Assert
            Assert.Equal(mockGameDomain.Name, mockGameDomainAct.Name);
            Assert.Equal(mockGameDomain.Genre, mockGameDomainAct.Genre);
            Assert.Equal(mockGameDomain.Price, mockGameDomainAct.Price);
            Assert.Equal(mockGameDomain.PromotionId, mockGameDomainAct.PromotionId);
            #endregion
        }
    }
}
