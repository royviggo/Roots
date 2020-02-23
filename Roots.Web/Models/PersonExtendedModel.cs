using Roots.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Roots.Web.Models
{
    public class PersonExtendedModel
    {
        public PersonExtendedModel()
        {
            Events = new List<EventModel>();
            Partners = new List<PartnerModel>();
        }

        public int Id { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        public Gender Gender { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        //public ChildModel Child { get; set; }
        public IList<EventModel> Events { get; set; }
        public IList<PartnerModel> Partners { get; set; }
    }
}
