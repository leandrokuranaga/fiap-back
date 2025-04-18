using Fiap.Domain.UserAggregate;
using Fiap.Infra.Data.MapEntities.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Infra.Data.MapEntities
{
    public class UsersMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Email)
                .IsUnique()
                .HasDatabaseName("IX_Users_Email");

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

            builder.HasMany(x => x.LibraryGames)
                   .WithOne(lg => lg.User)
                   .HasForeignKey(lg => lg.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(UserSeed.Users());
        }
    }
}
