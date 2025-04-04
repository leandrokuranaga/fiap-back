namespace Fiap.Application.Promotions.Models.Request
{
    public class CreatePromotionRequest
    {
        public double Discount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public List<int?>? GameId { get; set; }
    }
}
