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
            var purchaseDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var pricePaid = 59.99M;

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
            Assert.Equal(pricePaid, entity.PricePaid.Value);
            Assert.Equal(mockGame, entity.Game);
            Assert.Equal(mockUser, entity.User);
        }

        [Fact]
        public void LibraryGame_DefaultConstructor_ShouldAllowManualPropertyAssignment()
        {
            // Arrange
            var id = 1;
            var userId = 2;
            var gameId = 3;
            var purchaseDate = DateTime.UtcNow;
            var pricePaid = 99.99M;

            // Act                       
            var entity = new LibraryGame(id, userId, gameId, purchaseDate, pricePaid)
            {
                Game = new Domain.GameAggregate.Game { Id = 3, Name = "Manual Game" },
                User = new Domain.UserAggregate.User("Manual", "manual@email.com", "invalidHash@1salt", TypeUser.User, true)
                {
                    Id = 2
                }
            };
            
            // Assert
            Assert.Equal(id, entity.Id);
            Assert.Equal(userId, entity.UserId);
            Assert.Equal(gameId, entity.GameId);
            Assert.Equal(purchaseDate, entity.PurchaseDate);
            Assert.Equal(pricePaid, entity.PricePaid.Value);
            Assert.NotNull(entity.Game);
            Assert.NotNull(entity.User);
        }
    }
}
