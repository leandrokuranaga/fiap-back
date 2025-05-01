using Fiap.Domain.GameAggregate;
using Fiap.Domain.PromotionAggregate;
using Fiap.Domain.UserAggregate.Entities;
using Fiap.Domain.SeedWork.Exceptions;
using Fiap.Domain.Common.ValueObjects;

namespace Fiap.Tests._3._Domain_Layer_Tests
{
    public class GameDomainTest
    {
        [Fact]
        public void ParameterlessConstructor_ShouldCreateInstance()
        {
            var game = new Game();

            Assert.NotNull(game);
            Assert.Null(game.Name);
            Assert.Null(game.Genre);
            Assert.Equal(null, game.Price);
            Assert.Null(game.PromotionId);
            Assert.Null(game.Promotion);
            Assert.Null(game.Libraries);
        }

        [Fact]
        public void Constructor_WithBasicParameters_ShouldSetProperties()
        {
            var game = new Game("Game X", "Action", 49.99, null);

            Assert.Equal("Game X", game.Name);
            Assert.Equal("Action", game.Genre);
            Assert.Equal(49.99, game.Price.Value);
            Assert.Equal("BRL", game.Price.Currency);
            Assert.Null(game.PromotionId);
        }

        [Fact]
        public void Constructor_WithId_ShouldSetAllProperties()
        {
            var game = new Game(10, "Game With ID", "RPG", 59.99, 5);

            Assert.Equal(10, game.Id);
            Assert.Equal("Game With ID", game.Name);
            Assert.Equal("RPG", game.Genre);
            Assert.Equal(59.99, game.Price.Value);            
            Assert.Equal(5, game.PromotionId);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(double.MinValue)]
        public void Constructor_ShouldThrowException_WhenPriceIsNegative(double price)
        {
            var ex = Assert.Throws<BusinessRulesException>(() =>
                new Game("Game", "Action", price, null));

            Assert.Equal("The price must be greater than or equal to 0.", ex.Message);
        }

        [Fact]
        public void Constructor_ShouldAllow_WhenPriceIsZero()
        {
            var game = new Game("Free Game", "Indie", 0, null);

            Assert.Equal(0, game.Price.Value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_ShouldThrowException_WhenNameIsInvalid(string name)
        {
            var ex = Assert.Throws<BusinessRulesException>(() =>
                new Game(name, "Action", 49.99, null));

            Assert.Equal("The name of the game is required.", ex.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_ShouldThrowException_WhenGenreIsInvalid(string genre)
        {
            var ex = Assert.Throws<BusinessRulesException>(() =>
                new Game("Game", genre, 49.99, null));

            Assert.Equal("The genre of the game is required.", ex.Message);
        }

        [Fact]
        public void AssignPromotion_ShouldSetPromotionId()
        {
            var game = new Game("Game", "Puzzle", 19.99, null);
            game.AssignPromotion(5);

            Assert.Equal(5, game.PromotionId);
        }

        [Fact]
        public void Promotion_Property_ShouldWorkCorrectly()
        {
            var promotion = new Promotion(20, DateTime.UtcNow, DateTime.UtcNow.AddDays(7));
            var game = new Game { Promotion = promotion };

            Assert.NotNull(game.Promotion);
            Assert.Equal(20, game.Promotion.Discount);
        }

        [Fact]
        public void PromotionId_ShouldWork_WithoutPromotionObject()
        {
            var game = new Game { PromotionId = 5 };

            Assert.Equal(5, game.PromotionId);
            Assert.Null(game.Promotion);
        }

        [Fact]
        public void Should_Handle_Null_Promotion()
        {
            var game = new Game { Promotion = null };

            Assert.Null(game.Promotion);
        }

        [Fact]
        public void Libraries_ShouldInitialize_WhenAssigned()
        {
            var game = new Game();
            game.Libraries = new List<LibraryGame> { new() };

            Assert.NotNull(game.Libraries);
            Assert.Single(game.Libraries);
        }

        [Fact]
        public void Libraries_ShouldBeNull_ByDefault()
        {
            var game = new Game();

            Assert.Null(game.Libraries);
        }

        [Fact]
        public void Should_Add_To_Libraries()
        {
            var game = new Game { Libraries = new List<LibraryGame>() };
            var libraryGame = new LibraryGame();

            game.Libraries.Add(libraryGame);

            Assert.Contains(libraryGame, game.Libraries);
        }

        [Fact]
        public void Constructor_ShouldHandle_MaxDoublePrice()
        {
            var game = new Game("Expensive Game", "AAA", double.MaxValue, null);

            Assert.Equal(double.MaxValue, game.Price.Value);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenPriceIsNaN()
        {
            Assert.Throws<BusinessRulesException>(() =>
                new Game("Invalid Game", "Bug", double.NaN, null));
        }

        [Fact]
        public void Constructor_ShouldHandle_LongStrings()
        {
            var game = new Game(new string('A', 1000), new string('B', 500), 59.99, null);

            Assert.Equal(1000, game.Name.Length);
            Assert.Equal(500, game.Genre.Length);
        }

        [Fact]
        public void ValidatePrice_ShouldThrow_ForInvalidValues()
        {
            Assert.Throws<BusinessRulesException>(() =>
                new Game("Game", "Action", double.NaN, null));

            Assert.Throws<BusinessRulesException>(() =>
                new Game("Game", "Action", -1, null));
        }

        [Fact]
        public void ValidatePrice_ShouldPass_ForValidValues()
        {
            var game1 = new Game("Game1", "Action", 0, null);
            var game2 = new Game("Game2", "Action", 10.5, null);
            var game3 = new Game("Game3", "Action", double.MaxValue, null);

            Assert.Equal(0, game1.Price.Value);
            Assert.Equal(10.5, game2.Price.Value);
            Assert.Equal(double.MaxValue, game3.Price.Value);
        }

        [Fact]
        public void Libraries_ShouldBeInitializedBeforeUse()
        {
            var game = new Game();

            Assert.Null(game.Libraries);

            game.Libraries = new List<LibraryGame>();

            Assert.NotNull(game.Libraries);
            Assert.Empty(game.Libraries);
        }

        [Fact]
        public void Promotion_ShouldUpdateCorrectly_WhenReassigned()
        {
            var game = new Game();
            var promotion1 = new Promotion(10, DateTime.UtcNow, DateTime.UtcNow.AddDays(1));
            var promotion2 = new Promotion(20, DateTime.UtcNow, DateTime.UtcNow.AddDays(2));

            game.Promotion = promotion1;
            game.Promotion = promotion2;

            Assert.Equal(20, game.Promotion.Discount);
        }

        [Fact]
        public void Libraries_Setter_ShouldAcceptNewCollection()
        {
            var game = new Game();
            var newLibraries = new List<LibraryGame> { new LibraryGame() };

            game.Libraries = newLibraries;

            Assert.NotNull(game.Libraries);
            Assert.Single(game.Libraries);
        }

        [Fact]
        public void Promotion_Setter_ShouldHandleReassignment()
        {
            var game = new Game();
            var promo1 = new Promotion(10, DateTime.UtcNow, DateTime.UtcNow.AddDays(1));
            var promo2 = new Promotion(20, DateTime.UtcNow, DateTime.UtcNow.AddDays(2));

            game.Promotion = promo1;
            game.Promotion = promo2;

            Assert.Equal(20, game.Promotion.Discount);
        }

        [Fact]
        public void CreateGame_ShouldSetPriceWithCurrency()
        {
            // Arrange
            var name = "Test Game";
            var genre = "Action";
            var price = 99.99;
            var promotionId = (int?)null;

            // Act
            var game = new Game(name, genre, price, promotionId);

            // Assert
            Assert.Equal(name, game.Name);
            Assert.Equal(genre, game.Genre);
            Assert.Equal(price, game.Price.Value);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenCurrencyIsInvalid()
        {
            var ex = Assert.Throws<BusinessRulesException>(() =>
                new Game("Game", "Action", 49.99, null, "INVALID"));

            Assert.Equal("Invalid currency: INVALID. Supported currencies are: USD, EUR, BRL, JPY, GBP", ex.Message);
        }

        [Fact]
        public void Constructor_ShouldAllow_WhenCurrencyIsValid()
        {
            var game = new Game("Game", "Action", 49.99, null, "USD");

            Assert.Equal(49.99, game.Price.Value);
            Assert.Equal("USD", game.Price.Currency);
        }

        [Fact]
        public void CreateGame_ShouldSetPriceWithValidCurrency()
        {
            // Arrange
            var name = "Test Game";
            var genre = "Action";
            var price = 99.99;
            var promotionId = (int?)null;
            var currency = "USD";

            // Act
            var game = new Game(name, genre, price, promotionId, currency);

            // Assert
            Assert.Equal(name, game.Name);
            Assert.Equal(genre, game.Genre);
            Assert.Equal(price, game.Price.Value);
            Assert.Equal(currency, game.Price.Currency);
        }
    }
}
