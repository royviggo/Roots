using System;
using System.ComponentModel.DataAnnotations;

namespace Roots.Web.Models
{
    public class EventTypeModel
    {
        public int Id { get; set; }
        public bool IsFamilyEvent { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string GedcomTag { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
