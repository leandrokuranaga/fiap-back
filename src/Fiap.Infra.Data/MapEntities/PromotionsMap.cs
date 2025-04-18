using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Fiap.Infra.Data.MapEntities.Seeds;
using Fiap.Domain.Promotion;

namespace Fiap.Infra.Data.MapEntities
{
    public class PromotionsMap : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotion");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Discount).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();

            builder.HasData(PromotionSeed.Promotions());
        }
    }
}
