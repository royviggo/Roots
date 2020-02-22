using Roots.Domain.Common;
using System.Collections.Generic;

namespace Roots.Domain.Models
{
    public class Family : AuditableEntity
    {
        public Family()
        {
            Children = new HashSet<Child>();
        }

        public int Id { get; set; }
        public int FatherId { get; set; }
        public int MotherId { get; set; }

        public virtual Person Father { get; set; }
        public virtual Person Mother { get; set; }
        public virtual ICollection<Child> Children { get; set; }
    }
}
