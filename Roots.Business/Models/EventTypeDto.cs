using System;

namespace Roots.Business.Models
{
    public class EventTypeDto
    {
        public int Id { get; set; }
        public bool IsFamilyEvent { get; set; }
        public string Name { get; set; }
        public string GedcomTag { get; set; }
        public bool UseDate { get; set; }
        public bool UsePlace { get; set; }
        public bool UseDescription { get; set; }
        public string Sentence { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
