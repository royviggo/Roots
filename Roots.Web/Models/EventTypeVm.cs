using System;

namespace Roots.Web.Models
{
    public class EventTypeVm
    {
        public int Id { get; set; }
        public bool IsFamilyEvent { get; set; }
        public string Name { get; set; }
        public string GedcomTag { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
