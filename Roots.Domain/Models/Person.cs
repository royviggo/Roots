using System;
using System.Collections.Generic;

namespace Roots.Domain.Models
{
    public class Person
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
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}