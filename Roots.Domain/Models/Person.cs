using Roots.Domain.Common;
using Roots.Domain.Enums;
using System.Collections.Generic;

namespace Roots.Domain.Models
{
    public class Person : AuditableEntity
    {
        public Person()
        {
            Events = new HashSet<Event>();
            Partners = new HashSet<Partner>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public Status Status { get; set; }

        public virtual Child Child { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Partner> Partners { get; set; }
    }
}