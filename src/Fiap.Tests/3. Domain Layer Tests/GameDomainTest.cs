using Fiap.Domain.GameAggregate;
using Fiap.Domain.SeedWork.Exceptions;

namespace Fiap.Tests._3._Domain_Layer_Tests
{
    public class GameDomainTest
    {
        [Fact]
        public void GameDomain_ShouldCreateSuccessfully()
        {
            var name = "Game X";
            var genre = "Action"; 
            var price = 49.99;

            var game = new GameDomain(name, genre, price, null); 

            Assert.Equal(name, game.Name);
            Assert.Equal(genre, game.Genre); 
            Assert.Equal(price, game.Price);
        }

        [Fact]
        public void GameDomain_ShouldThrowException_WhenPriceIsNegative()
        {
            var name = "Game X";
            var genre = "Action";
            var price = -10;

            var ex = Assert.Throws<BusinessRulesException>(() =>
                new GameDomain(name, genre, price, null)); 

            Assert.Equal("The price of the game must be greater than or equal to 0.", ex.Message);


        }

        [Fact]
        public void GameDomain_ShouldCreateSuccessfully_WithPromotionId()
        {
            var name = "Game Y";
            var genre = "Adventure";
            var price = 59.99;
            var promotionId = 1;

            var game = new GameDomain(name, genre, price, promotionId);

            Assert.Equal(promotionId, game.PromotionId);
        }

        [Fact]
        public void AssignPromotion_ShouldSetPromotionId()
        {
            var game = new GameDomain("Game Z", "Puzzle", 19.99, null);
            var promotionId = 5;

            game.AssignPromotion(promotionId);

            Assert.Equal(promotionId, game.PromotionId);
        }

        [Fact]
        public void GameDomain_ShouldCreate_WhenPriceIsZero()
        {
            var game = new GameDomain("Free Game", "Indie", 0, null);

            Assert.Equal(0, game.Price);
        }


    }
}
