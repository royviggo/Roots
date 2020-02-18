using Roots.Domain.Enums;

namespace Roots.Business.Requests
{
    public class PersonUpdateRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public Status Status { get; set; }
    }
}
