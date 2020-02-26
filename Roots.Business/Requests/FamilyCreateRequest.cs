using System.Collections.Generic;

namespace Roots.Business.Requests
{
    public class FamilyCreateRequest
    {
        public int? FatherId { get; set; }
        public int? MotherId { get; set; }
        public ICollection<int> Children { get; set; }
    }
}
