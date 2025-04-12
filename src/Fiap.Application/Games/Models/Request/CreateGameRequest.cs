namespace Fiap.Application.Games.Models.Requests
{
    public class CreateGameRequest
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public int? PromotionId { get; set; }
    }
}
