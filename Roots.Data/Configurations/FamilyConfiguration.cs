using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roots.Domain.Models;

namespace Roots.Data.Configurations
{
    public class FamilyConfiguration : IEntityTypeConfiguration<Family>
    {
        public void Configure(EntityTypeBuilder<Family> builder)
        {
            builder.ToTable<Family>("Family");

            builder.Property(e => e.CreatedDate).HasColumnType("datetime");

            builder.Property(e => e.ModifiedDate).HasColumnType("datetime");

            builder.HasMany(c => c.Children)
                .WithOne(f => f.Family)
                .HasForeignKey(f => f.FamilyId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_Child_Family");

            builder.HasMany(c => c.Partners)
                .WithOne(f => f.Family)
                .HasForeignKey(f => f.FamilyId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_PersonFamily_Family");
        }
    }
}
