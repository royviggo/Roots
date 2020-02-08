using GenDateTools;
using System;

namespace Roots.Business.Models
{
    public class EventDto
    {
        public int Id { get; set; }
        public int EventTypeId { get; set; }
        public int PersonId { get; set; }
        public int PlaceId { get; set; }
        public GenDate EventDate { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public EventTypeDto EventType { get; set; }
        public PlaceDto Place { get; set; }
    }
}
