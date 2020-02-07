using AutoMapper;
using Roots.Business.Interfaces;
using Roots.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roots.Business.Models
{
    public class PersonDto
    {
        public PersonDto()
        {
            Events = new List<EventDto>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string Patronym { get; set; }
        public string LastName { get; set; }
        public int? Gender { get; set; }
        public int? BornYear { get; set; }
        public int? DeathYear { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public IList<EventDto> Events { get; set; }
    }
}
