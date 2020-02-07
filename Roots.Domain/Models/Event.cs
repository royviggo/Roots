using System;

namespace Roots.Domain.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int EventTypeId { get; set; }
        public int PersonId { get; set; }
        public int PlaceId { get; set; }
        public int DateDatetype { get; set; }
        public int DateDateFrom { get; set; }
        public int DateDateTo { get; set; }
        public string DateDatePhrase { get; set; }
        public string DateDateString { get; set; }
        public long? DateDateLong { get; set; }
        public bool? DateIsValid { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual EventType EventType { get; set; }
        public virtual Person Person { get; set; }
        public virtual Place Place { get; set; }
    }
}