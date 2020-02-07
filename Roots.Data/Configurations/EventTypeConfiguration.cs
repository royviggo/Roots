using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roots.Domain.Models;

namespace Roots.Data.Configurations
{
    public class EventTypeConfiguration : IEntityTypeConfiguration<EventType>
    {
        public void Configure(EntityTypeBuilder<EventType> builder)
        {
            builder.ToTable<EventType>("EventType");

            builder.Property(e => e.CreatedDate).HasColumnType("datetime");

            builder.Property(e => e.GedcomTag).HasMaxLength(255);

            builder.Property(e => e.ModifiedDate).HasColumnType("datetime");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Sentence).HasMaxLength(255);
        }
    }
}
