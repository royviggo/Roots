using Roots.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Roots.Business.Models
{
    public class PersonExtendedDto
    {
        public PersonExtendedDto()
        {
            Events = new List<EventDto>();
            Partners = new List<PartnerDto>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ChildDto Child { get; set; }
        public IList<EventDto> Events { get; set; }
        public IList<PartnerDto> Partners { get; set; }
    }
}
