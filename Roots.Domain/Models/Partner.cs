using Roots.Domain.Common;

namespace Roots.Domain.Models
{
    public class Partner : AuditableEntity
    {
        public int Id { get; set; }
        public int FamilyId { get; set; }
        public int PersonId { get; set; }
        public int PartnerRoleId { get; set; }

        public virtual Family Family { get; set; }
        public virtual Person Person { get; set; }
        public virtual PartnerRole PartnerRole { get; set; }
    }
}
