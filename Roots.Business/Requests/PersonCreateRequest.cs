namespace Roots.Business.Requests
{
    public class PersonCreateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Gender { get; set; }
        public int? Status { get; set; }
    }
}
