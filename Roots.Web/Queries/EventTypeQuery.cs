namespace Roots.Web.Queries
{
    public class EventTypeQuery : PagedQuery
    {
        public string Name { get; set; }
        public string GedcomTag { get; set; }
        public bool? IsFamilyEvent { get; set; }
    }
}