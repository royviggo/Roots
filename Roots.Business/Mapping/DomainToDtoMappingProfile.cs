using AutoMapper;
using GenDateTools;
using Roots.Business.Models;
using Roots.Domain.Models;

namespace Roots.Business.Mapping
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<string, GenDate>().ConvertUsing(s => ConvertStringToGenDate(s));
            CreateMap<GenDate, string>().ConvertUsing(g => ConvertGenDateToString(g));

            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<EventType, EventTypeDto>().ReverseMap();
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Place, PlaceDto>().ReverseMap();
        }

        private GenDate ConvertStringToGenDate(string value) => new GenDate(value);
        private string ConvertGenDateToString(GenDate genDate) => genDate.ToGenString();
    }
}
