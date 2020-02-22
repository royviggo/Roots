using System;

namespace Roots.Business.Models
{
    public class ChildDto
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public FamilyDto Family { get; set; }
        public PersonDto Person { get; set; }
    }
}
