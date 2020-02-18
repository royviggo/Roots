using Roots.Domain.Enums;

namespace Roots.Web.InputModels
{
    public class PersonCreateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public Status Status { get; set; }
    }
}