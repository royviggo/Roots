namespace Roots.Web.InputModels
{
    public class EventTypeUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GedcomTag { get; set; }
        public bool? IsFamilyEvent { get; set; }
    }
}
