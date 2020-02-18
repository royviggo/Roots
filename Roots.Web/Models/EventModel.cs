using GenDateTools;
using System;
using System.ComponentModel.DataAnnotations;

namespace Roots.Web.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public int EventTypeId { get; set; }
        public int PersonId { get; set; }
        public int PlaceId { get; set; }

        public GenDate EventDate { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public EventTypeModel EventType { get; set; }
        public PlaceModel Place { get; set; }
    }
}
