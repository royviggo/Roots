using GenDateTools;

namespace Roots.Business.Requests
{
    public class EventCreateRequest
    {
        public int EventTypeId { get; set; }
        public int PersonId { get; set; }
        public int PlaceId { get; set; }
        public GenDate EventDate { get; set; }
        public string Description { get; set; }
    }
}
