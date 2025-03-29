using Fiap.Domain.ContactAggregate;
using Fiap.Domain.EmailAggregate;
using Fiap.Domain.PhoneNumberAggregate;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Infra.Data
{
    public class Context(DbContextOptions<Context> options) : DbContext(options)
    {

        public DbSet<ContactDomain> Contact { get; set; }
        public DbSet<EmailDomain> Email { get; set; }
        public DbSet<PhoneNumberDomain> PhoneNumber { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MapEntities.ContactMap());
            modelBuilder.ApplyConfiguration(new MapEntities.EmailMap());
            modelBuilder.ApplyConfiguration(new MapEntities.PhoneNumberMap());
        }

    }
}
