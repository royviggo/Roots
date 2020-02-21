using Roots.Domain.Common;

namespace Roots.Domain.Models
{
    public class Family : AuditableEntity
    {
        public int Id { get; set; }
        public int FatherId { get; set; }
        public int MotherId { get; set; }
    }
}
