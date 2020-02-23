using System;

namespace Roots.Business.Models
{
    public class PartnerDto
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public FamilyDto Family { get; set; }
        public PersonDto Person { get; set; }
        public PartnerRoleDto PartnerRole { get; set; }
    }
}
