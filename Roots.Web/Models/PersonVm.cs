using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Roots.Web.Models
{
    public class PersonVm
    {
        public PersonVm()
        {
            Events = new List<EventVm>();
        }

        public int Id { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        public int? Gender { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public IList<EventVm> Events { get; set; }
    }
}
