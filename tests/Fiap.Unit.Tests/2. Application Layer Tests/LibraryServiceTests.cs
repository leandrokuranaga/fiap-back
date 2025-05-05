using Fiap.Application.Library.Models.Response;
using Fiap.Application.Library.Services;
using Fiap.Domain.LibraryAggregate;
using Fiap.Domain.SeedWork;
using Moq;

namespace Fiap.Unit.Tests._2._Application_Layer_Tests
{
    public class LibraryServiceTests
    {
        private readonly Mock<ILibraryRepository> _mockLibraryRepository;
        private readonly Mock<INotification> _mockNotification;
        private readonly LibraryService _libraryService;

        public LibraryServiceTests()
        {
            _mockLibraryRepository = new Mock<ILibraryRepository>();
            _mockNotification = new Mock<INotification>();
            _libraryService = new LibraryService(_mockNotification.Object, _mockLibraryRepository.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnLibraryResponse_WhenLibraryExists()
        {
            #region Arrange
            int userId = 1;
            var library = new LibraryResponse();
            library.UserId = userId;
            library.GameId = 1;
            library.PricePaid = 200.0;
            library.PurchaseDate = new DateTime(2024, 01, 07, 0, 0, 0);
            IEnumerable<LibraryResponse> libraryGame = new LibraryResponse[1] 
            { 
                library,
            };
            #endregion

            #region Act
            var result = await _libraryService.GetAllByUserLoggedAsync(userId);
            result.Select(r => r.GameId.Equals(1));
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(result, libraryGame);
            #endregion
        }
    }
}
