using Fiap.Domain.LibraryGameAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Infra.Data.MapEntities
{
    public class LibraryGameMap : IEntityTypeConfiguration<LibraryGameDomain>
    {
        public void Configure(EntityTypeBuilder<LibraryGameDomain> builder)
        {
            builder.ToTable("LibraryGames");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PurchaseDate).IsRequired();

            builder.Property(x => x.PricePaid).IsRequired();

            builder.HasOne(x => x.Library)
                   .WithMany(x => x.Games)
                   .HasForeignKey(x => x.LibraryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Game)
                   .WithMany(x => x.Libraries)
                   .HasForeignKey(x => x.GameId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
