using Fiap.Domain.LibraryAggregate;
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

            builder.HasOne(x => x.User)
                   .WithOne(u => u.Library)
                   .HasForeignKey<LibraryDomain>(x => x.UserId);

            builder.HasMany(x => x.Games)
                   .WithOne(g => g.Library)
                   .HasForeignKey(g => g.LibraryId);
        }

    }
}
