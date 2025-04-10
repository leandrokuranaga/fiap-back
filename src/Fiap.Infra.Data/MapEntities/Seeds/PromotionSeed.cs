using Fiap.Domain.PromotionAggregate;

namespace Fiap.Infra.Data.MapEntities.Seeds
{
    public static class PromotionSeed
    {
        public static List<PromotionDomain> Promotions()
        {
            return [
                new PromotionDomain(1,
                    DateTime.SpecifyKind(new DateTime(2025, 04, 01), DateTimeKind.Utc),
                    DateTime.SpecifyKind(new DateTime(2025, 05, 01), DateTimeKind.Utc))
                {
                    Id = 1
                },
                new PromotionDomain(2,
                    DateTime.SpecifyKind(new DateTime(2025, 06, 01), DateTimeKind.Utc),
                    DateTime.SpecifyKind(new DateTime(2025, 07, 01), DateTimeKind.Utc))
                {
                    Id = 2
                },
                new PromotionDomain(3,
                    DateTime.SpecifyKind(new DateTime(2025, 08, 01), DateTimeKind.Utc),
                    DateTime.SpecifyKind(new DateTime(2025, 09, 01), DateTimeKind.Utc))
                {
                    Id = 3
                }
            ];
        }

    }
}
