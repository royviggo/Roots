using System.Collections.Generic;

namespace Roots.Domain.Models
{
    public class PartnerRole
    {
        public PartnerRole()
        {
            Partners = new HashSet<Partner>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Partner> Partners { get; set; }
    }
}
