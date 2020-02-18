using System;
using System.ComponentModel.DataAnnotations;

namespace Roots.Web.Models
{
    public class PlaceModel
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
