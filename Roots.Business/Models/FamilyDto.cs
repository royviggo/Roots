using System;
using System.Collections.Generic;

namespace Roots.Business.Models
{
    public class FamilyDto
    {
        public FamilyDto()
        {
            Children = new List<ChildDto>();
        }

        public int Id { get; set; }
        public PersonDto Father { get; set; }
        public PersonDto Mother { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public IList<ChildDto> Children { get; set; }
    }
}
