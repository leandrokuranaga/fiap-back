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

                // senha F1ap@Senha
                pw.HasData(
                    new
                    {
                        UserId = 1,
                        Hash = "10000.LW59V9G+BlFV/Bb19uYa4g==.eYihrqMpMG7icxurO2Gz4Zf8XrqNxk+rWALXrqHmbgI=",
                        PasswordSalt = "LW59V9G+BlFV/Bb19uYa4g=="
                    },
                    new
                    {
                        UserId = 2,
                        Hash = "10000.V2BkMe/V+PQUC1g6VczN/g==.xAqE2zHO+O2FYokAs6Dn7DkHLaeVZ4xiJh7n8xF2rFg=",
                        PasswordSalt = "V2BkMe/V+PQUC1g6VczN/g=="
                    });
                });

            builder.HasData(UserSeed.Users());

        }
    }
}
