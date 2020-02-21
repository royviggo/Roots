using System;

namespace Roots.Domain.Models
{
    public class Child
    {
        public int Id { get; set; }
        public int FamilyId { get; set; }
        public int PersonId { get; set; }
        public int MyProperty { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
