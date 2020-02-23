using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Roots.Domain.Models;

namespace Roots.Data.Configurations
{
    public class PartnerRoleConfiguration : IEntityTypeConfiguration<PartnerRole>
    {
        public void Configure(EntityTypeBuilder<PartnerRole> builder)
        {
            builder.ToTable<PartnerRole>("PartnerRole");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
