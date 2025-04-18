using Fiap.Domain.UserAggregate.Entities;
using Fiap.Infra.Data.MapEntities.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Infra.Data.MapEntities
{
    public class LibraryGameMap : IEntityTypeConfiguration<LibraryGame>
    {
        public void Configure(EntityTypeBuilder<LibraryGame> builder)
        {
            builder.ToTable("LibraryGames");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.GameId)
                   .HasDatabaseName("IX_LibraryGames_GameId");

            builder.HasIndex(x => x.UserId)
                   .HasDatabaseName("IX_LibraryGames_UserId");

            builder.Property(x => x.PurchaseDate).IsRequired();

            builder.Property(x => x.PricePaid).IsRequired();

            builder.HasOne(x => x.Game)
                   .WithMany(x => x.Libraries)
                   .HasForeignKey(x => x.GameId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.LibraryGames)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(LibraryGameSeed.LibraryGames());
        }
    }
}
