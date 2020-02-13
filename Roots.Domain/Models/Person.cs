using Roots.Domain.Common;
using System.Collections.Generic;

namespace Roots.Domain.Models
{
    public class Person : AuditableEntity
    {
        public Person()
        {
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Gender { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}