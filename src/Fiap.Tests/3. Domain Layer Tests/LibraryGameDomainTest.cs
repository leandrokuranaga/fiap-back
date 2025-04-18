
using Fiap.Domain.UserAggregate.Entities;

namespace Fiap.Tests._3._Domain_Layer_Tests
{
    public class LibraryGameDomainTest
    {
        [Fact]
        public void LibraryGameDomainSuccess()
        {
            #region Arrange
            var mockLibraryGameDomain = new LibraryGame()
            {
                Id = 1,
                GameId = 1,
                UserId = 1,
                PurchaseDate = DateTime.Now,
                PricePaid = 70.0,
            };
            #endregion

            #region Act
            var mockLibraryGameDomainAct = new LibraryGame(
                mockLibraryGameDomain.Id,
                mockLibraryGameDomain.GameId,
                mockLibraryGameDomain.UserId,
                mockLibraryGameDomain.PurchaseDate,
                mockLibraryGameDomain.PricePaid
            );
            #endregion

            #region Assert
            Assert.Equal(mockLibraryGameDomain.Id, mockLibraryGameDomainAct.Id);
            Assert.Equal(mockLibraryGameDomain.GameId, mockLibraryGameDomainAct.GameId);
            Assert.Equal(mockLibraryGameDomain.UserId, mockLibraryGameDomainAct.UserId);
            Assert.Equal(mockLibraryGameDomain.PurchaseDate, mockLibraryGameDomainAct.PurchaseDate);
            Assert.Equal(mockLibraryGameDomain.PricePaid, mockLibraryGameDomainAct.PricePaid);
            #endregion
        }
    }
}
