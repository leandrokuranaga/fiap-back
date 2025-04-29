using Fiap.Domain.GameAggregate; 

namespace Fiap.Application.Games.Models.Response
{
    public record GameResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public int? PromotionId { get; set; }

        public static explicit operator GameResponse(Game game)
        {
            return new GameResponse
            {
                Id = game.Id,
                Name = game.Name,
                Genre = game.Genre,
                Price = game.Price,
                PromotionId = game.PromotionId
            };
        }
    }
}
