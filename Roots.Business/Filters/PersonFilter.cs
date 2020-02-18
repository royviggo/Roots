using Roots.Domain.Enums;

namespace Roots.Business.Filters
{
    public class PersonFilter : PaginationFilter
    {
        public PersonFilter() : base() { }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public Status Status { get; set; }
    }
}
