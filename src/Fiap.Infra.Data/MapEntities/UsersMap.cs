using Fiap.Domain.LibraryAggregate;
using Fiap.Domain.UserAggregate;
using Fiap.Infra.Data.MapEntities.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Infra.Data.MapEntities
{
    public class UsersMap : IEntityTypeConfiguration<UserDomain>
    {
        public void Configure(EntityTypeBuilder<UserDomain> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.TypeUser)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(x => x.Active)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasOne(x => x.Library)
                .WithOne(l => l.User)
                .HasForeignKey<LibraryDomain>(l => l.UserId);

            builder.HasData(UserSeed.Users());
        }
    }
}
