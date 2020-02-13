namespace Roots.Business.Requests
{
    public class PersonUpdateRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Gender { get; set; }
        public int? Status { get; set; }
    }
}
