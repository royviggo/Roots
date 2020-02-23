using System;

namespace Roots.Web.Models
{
    public class PartnerModel
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public FamilyModel Family { get; set; }
        public PersonModel Person { get; set; }
        public PartnerRoleModel PartnerRole { get; set; }
    }
}
