using System;

namespace Roots.Business.Models
{
    public class EventDto
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

        public EventTypeDto EventType { get; set; }
        public PlaceDto Place { get; set; }
    }
}
