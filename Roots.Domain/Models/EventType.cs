using Roots.Domain.Common;
using System.Collections.Generic;

namespace Roots.Domain.Models
{
    public class EventType : AuditableEntity
    {
        public EventType()
        {
            Event = new HashSet<Event>();
        }

        public int Id { get; set; }
        public bool IsFamilyEvent { get; set; }
        public string Name { get; set; }
        public string GedcomTag { get; set; }

        public virtual ICollection<Event> Event { get; set; }
    }
}