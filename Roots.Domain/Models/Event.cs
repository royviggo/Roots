﻿using Roots.Domain.Common;

namespace Roots.Domain.Models
{
    public class Event : AuditableEntity
    {
        public int Id { get; set; }
        public int EventTypeId { get; set; }
        public int PersonId { get; set; }
        public int PlaceId { get; set; }
        public long EventDate { get; set; }
        public string Description { get; set; }

        public virtual EventType EventType { get; set; }
        public virtual Person Person { get; set; }
        public virtual Place Place { get; set; }
    }
}