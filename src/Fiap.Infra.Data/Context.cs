using Fiap.Domain.Game;
using Fiap.Domain.Promotion;
using Fiap.Domain.UserAggregate;
using Fiap.Domain.UserAggregate.Entities;
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

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Promotion> Promotions { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!; 
        public DbSet<LibraryGame> LibraryGames { get; set; } = null!; 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersMap());
            modelBuilder.ApplyConfiguration(new PromotionsMap());
            modelBuilder.ApplyConfiguration(new GamesMap());
            modelBuilder.ApplyConfiguration(new LibraryGamesMap());
        }
    }
}
