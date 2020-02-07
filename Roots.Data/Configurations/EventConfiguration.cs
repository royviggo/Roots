using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roots.Domain.Models;

namespace Roots.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable<Event>("Event");

            builder.HasIndex(e => e.DateDateFrom)
                .HasName("IX_Event_DateFrom_NN");

            builder.HasIndex(e => e.DateDateLong)
                .HasName("IX_Event_DateLong_NN");

            builder.HasIndex(e => e.DateDateString)
                .HasName("IX_Event_DateString_NN");

            builder.HasIndex(e => e.DateDateTo)
                .HasName("IX_Event_DateTo_NN");

            builder.HasIndex(e => e.EventTypeId)
                .HasName("IX_Event_EventTypeId_NN");

            builder.HasIndex(e => e.PersonId)
                .HasName("IX_Event_PersonId_NN");

            builder.HasIndex(e => new { e.DateDateFrom, e.DateDateTo })
                .HasName("IX_Event_DateFrom_DateTo_NN");

            builder.HasIndex(e => new { e.EventTypeId, e.DateDateTo })
                .HasName("IX_Event_EventTypeId_DateTo_NN");

            builder.HasIndex(e => new { e.PersonId, e.EventTypeId })
                .HasName("IX_Event_EventTypeId_DateFrom_DateTo_NN");

            builder.Property(e => e.CreatedDate).HasColumnType("datetime");

            builder.Property(e => e.DateDateFrom).HasColumnName("Date_DateFrom");

            builder.Property(e => e.DateDateLong).HasColumnName("Date_DateLong");

            builder.Property(e => e.DateDatePhrase)
                .HasColumnName("Date_DatePhrase")
                .HasMaxLength(255);

            builder.Property(e => e.DateDateString)
                .HasColumnName("Date_DateString")
                .HasMaxLength(18)
                .IsUnicode(false);

            builder.Property(e => e.DateDateTo).HasColumnName("Date_DateTo");

            builder.Property(e => e.DateDatetype).HasColumnName("Date_Datetype");

            builder.Property(e => e.DateIsValid).HasColumnName("Date_IsValid");

            builder.Property(e => e.Description).HasMaxLength(255);

            builder.Property(e => e.ModifiedDate).HasColumnType("datetime");

            builder.HasOne(d => d.EventType)
                .WithMany(p => p.Event)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Event_EventType");

            builder.HasOne(d => d.Person)
                .WithMany(p => p.Events)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Event_Person");

            builder.HasOne(d => d.Place)
                .WithMany(p => p.Event)
                .HasForeignKey(d => d.PlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Event_Place");
        }
    }
}
