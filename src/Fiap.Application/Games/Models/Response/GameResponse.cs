namespace Fiap.Application.Games.Models.Responses
{
    public class GameResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public int? PromotionId { get; set; }
    }
}
