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
            CreateMap<long, GenDate>().ConvertUsing(l => ConvertLongToGenDate(l));
            CreateMap<GenDate, long>().ConvertUsing(g => ConvertGenDateToLong(g));

            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<EventType, EventTypeDto>().ReverseMap();
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Place, PlaceDto>().ReverseMap();
        }

        private static GenDate ConvertStringToGenDate(string value) => value != null ? new GenDate(value) : null;
        private static string ConvertGenDateToString(GenDate genDate) => genDate.ToGenString();
        private static GenDate ConvertLongToGenDate(long value) => new GenDate(value);
        private static long ConvertGenDateToLong(GenDate genDate) => genDate.DateLong;
    }
}
