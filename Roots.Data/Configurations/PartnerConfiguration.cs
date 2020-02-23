using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roots.Domain.Models;

namespace Roots.Data.Configurations
{
    public class PartnerConfiguration : IEntityTypeConfiguration<Partner>
    {
        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            builder.ToTable<Partner>("Partner");

            builder.Property(e => e.CreatedDate).HasColumnType("datetime");

            builder.Property(e => e.ModifiedDate).HasColumnType("datetime");

            builder.HasOne(c => c.Family)
                .WithMany(f => f.Partners)
                .HasForeignKey(c => c.FamilyId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_Partner_Family");

            builder.HasOne(d => d.Person)
                .WithMany(p => p.Partners)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_Partner_Person");

            builder.HasOne(d => d.PartnerRole)
                .WithMany(p => p.Partners)
                .HasForeignKey(d => d.PartnerRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Partner_ParentRole");
        }
    }
}
