using AutoMapper;
using Roots.Business.Models;
using Roots.Web.Models;

namespace Roots.Web.Mapping
{
    public class DtoToVmMappingProfile : Profile
    {
        public DtoToVmMappingProfile()
        {
            CreateMap<EventDto, EventModel>().ReverseMap();
            CreateMap<EventTypeDto, EventTypeModel>().ReverseMap();
            CreateMap<PersonDto, PersonModel>().ReverseMap();
            CreateMap<PlaceDto, PlaceModel>().ReverseMap();
        }
    }
}
