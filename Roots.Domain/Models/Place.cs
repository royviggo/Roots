using System;
using System.Collections.Generic;

namespace Roots.Domain.Models
{
    public class Place
    {
        public Place()
        {
            Event = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<Event> Event { get; set; }
    }
}