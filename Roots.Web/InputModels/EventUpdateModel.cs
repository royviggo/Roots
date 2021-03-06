﻿using GenDateTools;
using System.ComponentModel.DataAnnotations;

namespace Roots.Web.InputModels
{
    public class EventUpdateModel
    {
        public int Id { get; set; }
        public int EventTypeId { get; set; }
        public int PersonId { get; set; }
        public int PlaceId { get; set; }

        public GenDate EventDate { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
    }
}
