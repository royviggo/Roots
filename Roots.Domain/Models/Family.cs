using Roots.Domain.Common;
using System.Collections.Generic;

namespace Roots.Domain.Models
{
    public class Family : AuditableEntity
    {
        public Family()
        {
            Children = new HashSet<Child>();
            Partners = new HashSet<Partner>();
        }

        public int Id { get; set; }

        public virtual ICollection<Partner> Partners { get; set; }
        public virtual ICollection<Child> Children { get; set; }
    }
}
