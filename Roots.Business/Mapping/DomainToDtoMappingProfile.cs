using AutoMapper;
using Roots.Business.Models;
using Roots.Domain.Models;

namespace Roots.Business.Mapping
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<EventType, EventTypeDto>().ReverseMap();
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Place, PlaceDto>().ReverseMap();
        }
    }
}
