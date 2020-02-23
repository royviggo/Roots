using AutoMapper;
using Roots.Business.Models;
using Roots.Web.Models;

namespace Roots.Web.Mapping
{
    public class DtoToVmMappingProfile : Profile
    {
        public DtoToVmMappingProfile()
        {
            CreateMap<ChildDto, ChildModel>().ReverseMap();
            CreateMap<EventDto, EventModel>().ReverseMap();
            CreateMap<EventTypeDto, EventTypeModel>().ReverseMap();
            CreateMap<FamilyDto, FamilyModel>().ReverseMap();
            CreateMap<PartnerDto, PartnerModel>().ReverseMap();
            CreateMap<PartnerRoleDto, PartnerRoleModel>().ReverseMap();
            CreateMap<PersonDto, PersonModel>().ReverseMap();
            CreateMap<PlaceDto, PlaceModel>().ReverseMap();
        }
    }
}
