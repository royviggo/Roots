using GenDateTools;

namespace Roots.Business.Filters
{
    public class EventFilter : PaginationFilter
    {
        public EventFilter() : base() { }
        public GenDate DateFrom { get; set; }
        public GenDate DateTo { get; set; }
        public int? EventType { get; set; }
    }
}
