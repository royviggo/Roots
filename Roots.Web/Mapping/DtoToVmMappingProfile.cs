using AutoMapper;
using Roots.Business.Models;
using Roots.Web.Models;

namespace Roots.Web.Mapping
{
    public class DtoToVmMappingProfile : Profile
    {
        public DtoToVmMappingProfile()
        {
            CreateMap<EventDto, EventVm>().ReverseMap();
            CreateMap<EventTypeDto, EventTypeVm>().ReverseMap();
            CreateMap<PersonDto, PersonVm>().ReverseMap();
            CreateMap<PlaceDto, PlaceVm>().ReverseMap();
        }
    }
}
