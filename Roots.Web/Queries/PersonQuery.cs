using Roots.Domain.Enums;

namespace Roots.Web.Queries
{
    public class PersonQuery : PagedQuery
    {
        public PersonQuery() { }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public Status Status { get; set; }
    }
}
