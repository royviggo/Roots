using Roots.Business.Models;

namespace Roots.Business.Filters
{
    public class EventFilter : PaginationFilter
    {
        public EventFilter() : base() { }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int? EventType { get; set; }
    }
}
