using Fiap.Domain.GameAggregate;
using Fiap.Domain.LibraryAggregate;
using Fiap.Domain.LibraryGameAggregate;
using Fiap.Domain.PromotionAggregate;
using Fiap.Domain.UserAggregate;
using Fiap.Infra.Data.MapEntities;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Infra.Data
{
    public class Context : DbContext 
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<UserDomain> Users { get; set; } = null!;
        public DbSet<PromotionDomain> Promotions { get; set; } = null!;
        public DbSet<GameDomain> Games { get; set; } = null!; 
        public DbSet<LibraryDomain> Libraries { get; set; } = null!; 
        public DbSet<LibraryGameDomain> LibraryGames { get; set; } = null!; 

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
