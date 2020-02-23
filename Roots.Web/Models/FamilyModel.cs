using System;
using System.Collections.Generic;

namespace Roots.Web.Models
{
    public class FamilyModel
    {
        public FamilyModel()
        {
            Children = new List<ChildModel>();
            Partners = new List<PartnerModel>();
        }

        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public IList<PartnerModel> Partners { get; set; }
        public IList<ChildModel> Children { get; set; }
    }
}
