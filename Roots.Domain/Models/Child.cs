using Roots.Domain.Common;

namespace Roots.Domain.Models
{
    public class Child : AuditableEntity
    {
        public int Id { get; set; }
        public int FamilyId { get; set; }
        public int PersonId { get; set; }
        public int MyProperty { get; set; }

        public virtual Family Family { get; set; }
        public virtual Person Person { get; set; }
    }
}
