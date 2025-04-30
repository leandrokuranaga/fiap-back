using Fiap.Domain.UserAggregate.Entities;
using Fiap.Domain.UserAggregate.Enums;
using Xunit;

namespace Fiap.Tests._3._Domain_Layer_Tests
{
    public class LibraryGameDomainTest
    {
        [Fact]
        public void LibraryGame_ShouldInitializeAndAssignPropertiesCorrectly()
        {
            // Arrange
            var id = 1;
            var userId = 10;
            var gameId = 20;
            var purchaseDate = new DateTime(2024, 1, 1);
            var pricePaid = 59.99;

            var mockGame = new Fiap.Domain.GameAggregate.Game { Id = gameId, Name = "Mock Game" };
            var mockUser = new Fiap.Domain.UserAggregate.User("Mock", "mock@email.com", "invalidHash@1salt", TypeUser.User, true)
            {
                Id = userId
            };

            // Act
            var entity = new LibraryGame(id, userId, gameId, purchaseDate, pricePaid)
            {
                Game = mockGame,
                User = mockUser
            };

            // Assert
            Assert.Equal(id, entity.Id);
            Assert.Equal(userId, entity.UserId);
            Assert.Equal(gameId, entity.GameId);
            Assert.Equal(purchaseDate, entity.PurchaseDate);
            Assert.Equal(pricePaid, entity.PricePaid);
            Assert.Equal(mockGame, entity.Game);
            Assert.Equal(mockUser, entity.User);
        }

        [Fact]
        public void LibraryGame_DefaultConstructor_ShouldAllowManualPropertyAssignment()
        {
            // Arrange
            var entity = new LibraryGame();

            var purchaseDate = DateTime.UtcNow;

            // Act
            entity.Id = 1;
            entity.UserId = 2;
            entity.GameId = 3;
            entity.PurchaseDate = purchaseDate;
            entity.PricePaid = 99.99;
            entity.Game = new Fiap.Domain.GameAggregate.Game { Id = 3, Name = "Manual Game" };
            entity.User = new Fiap.Domain.UserAggregate.User("Manual", "manual@email.com", "invalidHash@1salt", TypeUser.User, true)
            {
                Id = 2
            };

            // Assert
            Assert.Equal(1, entity.Id);
            Assert.Equal(2, entity.UserId);
            Assert.Equal(3, entity.GameId);
            Assert.Equal(purchaseDate, entity.PurchaseDate);
            Assert.Equal(99.99, entity.PricePaid);
            Assert.NotNull(entity.Game);
            Assert.NotNull(entity.User);
        }
    }
}
