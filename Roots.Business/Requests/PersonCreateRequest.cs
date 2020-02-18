using Roots.Domain.Enums;

namespace Roots.Business.Requests
{
    public class PersonCreateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public Status Status { get; set; }
    }
}
