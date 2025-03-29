using Fiap.Domain.ContactAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Infra.Data.MapEntities
{
    public class ContactMap : IEntityTypeConfiguration<ContactDomain>
    {
        public void Configure(EntityTypeBuilder<ContactDomain> builder)
        {
            builder.ToTable("Contact");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(x => x.Emails)
                .WithOne(x => x.Contact)
                .HasForeignKey(x => x.ContactId);

            builder.HasMany(x => x.PhoneNumbers)
                .WithOne(x => x.Contact)
                .HasForeignKey(x => x.ContactId);
        }
    }
}
