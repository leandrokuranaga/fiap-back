using Fiap.Domain.LibraryAggregate;

namespace Fiap.Tests._3._Domain_Layer_Tests
{
    public class LibraryDomainTest
    {
        [Fact]
        public void LibraryDomainSuccess()
        {
            #region Arrange
            var mockLibraryDomain = new LibraryDomain()
            {
                UserId = 1
            };
            #endregion

            #region Act
            var mockLibraryDomainAct = new LibraryDomain(
                mockLibraryDomain.UserId
            );
            #endregion

            #region Assert
            Assert.Equal(mockLibraryDomain.UserId, mockLibraryDomainAct.UserId);
            #endregion
        }
    }
}
