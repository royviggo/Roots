using System;
using System.Collections.Generic;

namespace Roots.Business.Models
{
    public class FamilyDto
    {
        public FamilyDto()
        {
            Children = new List<ChildDto>();
            Partners = new List<PartnerDto>();
        }

        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public IList<PartnerDto> Partners { get; set; }
        public IList<ChildDto> Children { get; set; }
    }
}
