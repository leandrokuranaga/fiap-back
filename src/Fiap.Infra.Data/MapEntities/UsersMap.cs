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

            builder.OwnsOne(x => x.Password, pw =>
            {
                pw.Property(p => p.Hash)
                  .HasColumnName("PasswordHash")
                  .IsRequired();

                pw.Property(p => p.PasswordSalt)
                  .HasColumnName("PasswordSalt")
                  .IsRequired();

                pw.WithOwner();

                pw.HasData(
                    new
                    {
                        UserId = 1,
                        Hash = "10000.6O0ksK7RcY+koP2vTclK0g==.Tu79I/VFmqjTUFuGlTKTOqpR2zovm2jPrEVn4sUYXXw=",
                        PasswordSalt = "6O0ksK7RcY+koP2vTclK0g=="
                    },
                    new
                    {
                        UserId = 2,
                        Hash = "10000.8WQ7yoG2Z4EyAwT9lHpOgg==.THjXUlDZ5dyMBgkDZpHZ6UD22O6GZwSR6s1FFgrTNU0=",
                        PasswordSalt = "8WQ7yoG2Z4EyAwT9lHpOgg=="
                    });
            });

            builder.HasData(UserSeed.Users());

        }
    }
}
