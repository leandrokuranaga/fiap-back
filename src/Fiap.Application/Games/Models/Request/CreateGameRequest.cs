using Fiap.Domain.GameAggregate;

namespace Fiap.Application.Games.Models.Request

{
    public record CreateGameRequest
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public int? PromotionId { get; set; }

        public static explicit operator GameDomain(CreateGameRequest c)
        {
            return new GameDomain(
                name: c.Name,
                genre: c.Genre,
                price: c.Price,
                promotionId: c.PromotionId
            );
        }
    }
}
