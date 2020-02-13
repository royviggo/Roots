using Roots.Domain.Common;
using System.Collections.Generic;

namespace Roots.Domain.Models
{
    public class Place : AuditableEntity
    {
        public Place()
        {
            Event = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Event> Event { get; set; }
    }
}