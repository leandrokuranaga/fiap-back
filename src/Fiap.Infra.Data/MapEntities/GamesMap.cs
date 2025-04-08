using Fiap.Domain.GameAggregate;
using Fiap.Infra.Data.MapEntities.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Infra.Data.MapEntities
{
    public class GamesMap : IEntityTypeConfiguration<GameDomain>
    {
        public void Configure(EntityTypeBuilder<GameDomain> builder)
        {
            builder.ToTable("Games");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Genre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Price)
                .IsRequired();

            builder.HasOne(x => x.Promotion)
                   .WithMany(p => p.Games)
                   .HasForeignKey(x => x.PromotionId);

            builder.HasData(GameSeed.Game());
        }
    }
}
