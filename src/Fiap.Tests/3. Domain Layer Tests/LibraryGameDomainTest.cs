using Fiap.Domain.LibraryGameAggregate;

namespace Fiap.Tests._3._Domain_Layer_Tests
{
    public class LibraryGameDomainTest
    {
        [Fact]
        public void LibraryGameDomainSuccess()
        {
            #region Arrange
            var mockLibraryGameDomain = new LibraryGameDomain()
            {
                GameId = 1,
                LibraryId = 1,
                PurchaseDate = DateTime.Now,
                PricePaid = 70.0,
            };
            #endregion

            #region Act
            var mockLibraryGameDomainAct = new LibraryGameDomain(
                mockLibraryGameDomain.GameId,
                mockLibraryGameDomain.LibraryId,
                mockLibraryGameDomain.PurchaseDate,
                mockLibraryGameDomain.PricePaid
            );
            #endregion

            #region Assert
            Assert.Equal(mockLibraryGameDomain.GameId, mockLibraryGameDomainAct.GameId);
            Assert.Equal(mockLibraryGameDomain.LibraryId, mockLibraryGameDomainAct.LibraryId);
            Assert.Equal(mockLibraryGameDomain.PurchaseDate, mockLibraryGameDomainAct.PurchaseDate);
            Assert.Equal(mockLibraryGameDomain.PricePaid, mockLibraryGameDomainAct.PricePaid);
            #endregion
        }
    }
}
