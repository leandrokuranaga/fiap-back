using Fiap.Domain.GameAggregate;
using Fiap.Domain.LibraryAggregate;
using Fiap.Domain.LibraryGameAggregate;
using Fiap.Domain.PromotionAggregate;
using Fiap.Domain.UserAggregate;
using Fiap.Infra.Data.MapEntities;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Infra.Data
{
    public class Context(DbContextOptions<Context> options) : DbContext(options)
    {

        public DbSet<UserDomain> Users { get; set; }
        public DbSet<PromotionDomain> Promotions { get; set; }
        public DbSet<GameDomain> Games { get; set; }
        public DbSet<LibraryDomain> Libraries { get; set; }
        public DbSet<LibraryGameDomain> LibraryGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersMap());
            modelBuilder.ApplyConfiguration(new PromotionsMap());
            modelBuilder.ApplyConfiguration(new GamesMap());
            modelBuilder.ApplyConfiguration(new LibrariesMap());
            modelBuilder.ApplyConfiguration(new LibraryGameMap());
        }

    }
}
