namespace Roots.Web.Queries
{
    public class EventQuery : PagedQuery
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int? EventType { get; set; }
    }
}