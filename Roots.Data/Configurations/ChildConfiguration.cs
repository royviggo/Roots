using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roots.Domain.Models;

namespace Roots.Data.Configurations
{
    public class ChildConfiguration : IEntityTypeConfiguration<Child>
    {
        public void Configure(EntityTypeBuilder<Child> builder)
        {
            builder.ToTable<Child>("Child");

            builder.Property(e => e.CreatedDate).HasColumnType("datetime");

            builder.Property(e => e.ModifiedDate).HasColumnType("datetime");

            builder.HasOne(c => c.Family)
                .WithMany(f => f.Children)
                .HasForeignKey(c => c.FamilyId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_Child_Family");

            builder.HasOne(d => d.Person)
                .WithOne(p => p.Child)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_Child_Person");
        }
    }
}
