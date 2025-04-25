using Fiap.Domain.GameAggregate;
using Fiap.Domain.LibraryGameAggregate;
using Fiap.Domain.PromotionAggregate;
using Fiap.Domain.SeedWork.Exceptions;
using Xunit;

namespace Fiap.Tests._3._Domain_Layer_Tests
{
    public class GameDomainTest
    {
        [Fact]
        public void ParameterlessConstructor_ShouldCreateInstance()
        {
            var game = new GameDomain();

            Assert.NotNull(game);
            Assert.Null(game.Name);
            Assert.Null(game.Genre);
            Assert.Equal(0, game.Price);
            Assert.Null(game.PromotionId);
            Assert.Null(game.Promotion);
            Assert.Null(game.Libraries);
        }

        [Fact]
        public void Constructor_WithBasicParameters_ShouldSetProperties()
        {
            var game = new GameDomain("Game X", "Action", 49.99, null);

            Assert.Equal("Game X", game.Name);
            Assert.Equal("Action", game.Genre);
            Assert.Equal(49.99, game.Price);
            Assert.Null(game.PromotionId);
        }

        [Fact]
        public void Constructor_WithId_ShouldSetAllProperties()
        {
            var game = new GameDomain(10, "Game With ID", "RPG", 59.99, 5);

            Assert.Equal(10, game.Id);
            Assert.Equal("Game With ID", game.Name);
            Assert.Equal("RPG", game.Genre);
            Assert.Equal(59.99, game.Price);
            Assert.Equal(5, game.PromotionId);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(double.MinValue)]
        public void Constructor_ShouldThrowException_WhenPriceIsNegative(double price)
        {
            var ex = Assert.Throws<BusinessRulesException>(() =>
                new GameDomain("Game", "Action", price, null));

            Assert.Equal("The price of the game must be greater than or equal to 0.", ex.Message);
        }

        [Fact]
        public void Constructor_ShouldAllow_WhenPriceIsZero()
        {
            var game = new GameDomain("Free Game", "Indie", 0, null);

            Assert.Equal(0, game.Price);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_ShouldThrowException_WhenNameIsInvalid(string name)
        {
            var ex = Assert.Throws<BusinessRulesException>(() =>
                new GameDomain(name, "Action", 49.99, null));

            Assert.Equal("The name of the game is required.", ex.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_ShouldThrowException_WhenGenreIsInvalid(string genre)
        {
            var ex = Assert.Throws<BusinessRulesException>(() =>
                new GameDomain("Game", genre, 49.99, null));

            Assert.Equal("The genre of the game is required.", ex.Message);
        }

        [Fact]
        public void AssignPromotion_ShouldSetPromotionId()
        {
            var game = new GameDomain("Game", "Puzzle", 19.99, null);
            game.AssignPromotion(5);

            Assert.Equal(5, game.PromotionId);
        }

        [Fact]
        public void Promotion_Property_ShouldWorkCorrectly()
        {
            var promotion = new PromotionDomain(20, DateTime.Now, DateTime.Now.AddDays(7));
            var game = new GameDomain { Promotion = promotion };

            Assert.NotNull(game.Promotion);
            Assert.Equal(20, game.Promotion.Discount);
        }

        [Fact]
        public void PromotionId_ShouldWork_WithoutPromotionObject()
        {
            var game = new GameDomain { PromotionId = 5 };

            Assert.Equal(5, game.PromotionId);
            Assert.Null(game.Promotion);
        }

        [Fact]
        public void Should_Handle_Null_Promotion()
        {
            var game = new GameDomain { Promotion = null };

            Assert.Null(game.Promotion);
        }

        [Fact]
        public void Libraries_ShouldInitialize_WhenAssigned()
        {
            var game = new GameDomain();
            game.Libraries = new List<LibraryGameDomain> { new() };

            Assert.NotNull(game.Libraries);
            Assert.Single(game.Libraries);
        }

        [Fact]
        public void Libraries_ShouldBeNull_ByDefault()
        {
            var game = new GameDomain();

            Assert.Null(game.Libraries);
        }

        [Fact]
        public void Should_Add_To_Libraries()
        {
            var game = new GameDomain { Libraries = new List<LibraryGameDomain>() };
            var libraryGame = new LibraryGameDomain();

            game.Libraries.Add(libraryGame);

            Assert.Contains(libraryGame, game.Libraries);
        }

        [Fact]
        public void Constructor_ShouldHandle_MaxDoublePrice()
        {
            var game = new GameDomain("Expensive Game", "AAA", double.MaxValue, null);

            Assert.Equal(double.MaxValue, game.Price);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenPriceIsNaN()
        {
            Assert.Throws<BusinessRulesException>(() =>
                new GameDomain("Invalid Game", "Bug", double.NaN, null));
        }

        [Fact]
        public void Constructor_ShouldHandle_LongStrings()
        {
            var game = new GameDomain(new string('A', 1000), new string('B', 500), 59.99, null);

            Assert.Equal(1000, game.Name.Length);
            Assert.Equal(500, game.Genre.Length);
        }

        [Fact]
        public void ValidatePrice_ShouldThrow_ForInvalidValues()
        {
            Assert.Throws<BusinessRulesException>(() =>
                new GameDomain("Game", "Action", double.NaN, null));

            Assert.Throws<BusinessRulesException>(() =>
                new GameDomain("Game", "Action", -1, null));
        }

        [Fact]
        public void ValidatePrice_ShouldPass_ForValidValues()
        {
            var game1 = new GameDomain("Game1", "Action", 0, null);
            var game2 = new GameDomain("Game2", "Action", 10.5, null);
            var game3 = new GameDomain("Game3", "Action", double.MaxValue, null);

            Assert.Equal(0, game1.Price);
            Assert.Equal(10.5, game2.Price);
            Assert.Equal(double.MaxValue, game3.Price);
        }

        [Fact]
        public void Libraries_ShouldBeInitializedBeforeUse()
        {
            var game = new GameDomain();

            Assert.Null(game.Libraries);

            game.Libraries = new List<LibraryGameDomain>();

            Assert.NotNull(game.Libraries);
            Assert.Empty(game.Libraries);
        }

        [Fact]
        public void Promotion_ShouldUpdateCorrectly_WhenReassigned()
        {
            var game = new GameDomain();
            var promotion1 = new PromotionDomain(10, DateTime.Now, DateTime.Now.AddDays(1));
            var promotion2 = new PromotionDomain(20, DateTime.Now, DateTime.Now.AddDays(2));

            game.Promotion = promotion1;
            game.Promotion = promotion2;

            Assert.Equal(20, game.Promotion.Discount);
        }

        [Fact]
        public void Libraries_Setter_ShouldAcceptNewCollection()
        {
            var game = new GameDomain();
            var newLibraries = new List<LibraryGameDomain> { new LibraryGameDomain() };

            game.Libraries = newLibraries;

            Assert.NotNull(game.Libraries);
            Assert.Single(game.Libraries);
        }

        [Fact]
        public void Promotion_Setter_ShouldHandleReassignment()
        {
            var game = new GameDomain();
            var promo1 = new PromotionDomain(10, DateTime.Now, DateTime.Now.AddDays(1));
            var promo2 = new PromotionDomain(20, DateTime.Now, DateTime.Now.AddDays(2));

            game.Promotion = promo1;
            game.Promotion = promo2;

            Assert.Equal(20, game.Promotion.Discount);
        }
    }
}