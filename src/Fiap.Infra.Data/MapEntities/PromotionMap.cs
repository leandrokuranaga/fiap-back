using Fiap.Domain.PromotionAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Infra.Data.MapEntities
{
    public class PromotionMap : IEntityTypeConfiguration<PromotionDomain>
    {
        public void Configure(EntityTypeBuilder<PromotionDomain> builder)
        {
            builder.ToTable("Promotion");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Discount).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
        }
    }
}
