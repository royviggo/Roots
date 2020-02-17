namespace Roots.Web.InputModels
{
    public class PersonCreateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Gender { get; set; }
        public int? Status { get; set; }
    }
}