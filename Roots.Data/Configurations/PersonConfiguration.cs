using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roots.Domain.Models;

namespace Roots.Data.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable<Person>("Person");

            builder.Property(e => e.FirstName).HasMaxLength(255);

            builder.Property(e => e.LastName).HasMaxLength(255);

            builder.Property(e => e.FatherName).HasMaxLength(255);

            builder.Property(e => e.Patronym).HasMaxLength(255);

            builder.Property(e => e.CreatedDate).HasColumnType("datetime");

            builder.Property(e => e.ModifiedDate).HasColumnType("datetime");
        }
    }
}
