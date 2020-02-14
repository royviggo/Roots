namespace Roots.Business.Requests
{
    public class EventTypeCreateRequest
    {
        public bool IsFamilyEvent { get; set; }
        public string Name { get; set; }
        public string GedcomTag { get; set; }
    }
}
