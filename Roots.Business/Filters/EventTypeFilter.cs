namespace Roots.Business.Filters
{
    public class EventTypeFilter : PaginationFilter
    {
        public EventTypeFilter() : base() { }
        public bool? IsFamilyEvent { get; set; }
        public string Name { get; set; }
        public string GedcomTag { get; set; }
    }
}
