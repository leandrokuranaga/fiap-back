using Fiap.Domain.LibraryAggregate;
using Fiap.Infra.Data.MapEntities.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Infra.Data.MapEntities
{
    public class LibrariesMap : IEntityTypeConfiguration<LibraryDomain>
    {
        public void Configure(EntityTypeBuilder<LibraryDomain> builder)
        {
            builder.ToTable("Library");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.UserId)
                   .HasDatabaseName("IX_Library_UserId");

            builder.HasOne(x => x.User)
                   .WithOne(u => u.Library)
                   .HasForeignKey<LibraryDomain>(x => x.UserId);

            builder.HasMany(x => x.Games)
                   .WithOne(g => g.Library)
                   .HasForeignKey(g => g.LibraryId);

            builder.HasData(LibrarySeed.Libraries());
        }

    }
}
