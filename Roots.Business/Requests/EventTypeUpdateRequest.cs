namespace Roots.Business.Requests
{
    public class EventTypeUpdateRequest
    {
        public int Id { get; set; }
        public bool IsFamilyEvent { get; set; }
        public string Name { get; set; }
        public string GedcomTag { get; set; }
    }
}
