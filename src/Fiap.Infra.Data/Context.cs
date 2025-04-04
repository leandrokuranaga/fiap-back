using Fiap.Domain.GameAggregate;
using Fiap.Domain.LibraryAggregate;
using Fiap.Domain.PromotionAggregate;
using Fiap.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Infra.Data
{
    public class Context(DbContextOptions<Context> options) : DbContext(options)
    {

        public DbSet<UserDomain> Users { get; set; }
        public DbSet<LibraryDomain> Libraries { get; set; }
        public DbSet<GameDomain> Games { get; set; }
        public DbSet<PromotionDomain> Promotions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MapEntities.UsersMap());
            modelBuilder.ApplyConfiguration(new MapEntities.GamesMap());
            modelBuilder.ApplyConfiguration(new MapEntities.LibrariesMap());
            modelBuilder.ApplyConfiguration(new MapEntities.LibraryGameMap());
            modelBuilder.ApplyConfiguration(new MapEntities.PromotionMap());
        }

    }
}
