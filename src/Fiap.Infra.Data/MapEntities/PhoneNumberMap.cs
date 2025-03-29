using Fiap.Domain.PhoneNumberAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Infra.Data.MapEntities
{
    public class PhoneNumberMap : IEntityTypeConfiguration<PhoneNumberDomain>
    {
        public void Configure(EntityTypeBuilder<PhoneNumberDomain> builder)
        {
            builder.Property(p => p.PhoneNumber)
                   .HasMaxLength(20);

            builder.HasKey(x => x.Id);

            builder.Property(p => p.DDD)
                     .HasMaxLength(2);

            builder.Property(p => p.State)
                        .HasMaxLength(2);

            builder.HasOne(p => p.Contact)
                     .WithMany(p => p.PhoneNumbers)
                     .HasForeignKey(p => p.ContactId);
        }
    }
}
