using AutoMapper;
using Roots.Business.Models;
using Roots.Business.Responses;
using Roots.Web.Models;
using Roots.Web.Responses;
using System.Collections.Generic;

namespace Roots.Web.Mapping
{
    public class PagedToResponseMappingProfile : Profile
    {
        public PagedToResponseMappingProfile()
        {
            CreateMap<Paged<IEnumerable<PersonDto>>, PagedResponse<IEnumerable<PersonModel>>>()
                .ForMember(dest => dest.Page, map => map.MapFrom(source => source.PageNumber))
                .ForMember(dest => dest.Limit, map => map.MapFrom(source => source.PageSize));

            CreateMap<Paged<IEnumerable<EventDto>>, PagedResponse<IEnumerable<EventModel>>>()
                .ForMember(dest => dest.Page, map => map.MapFrom(source => source.PageNumber))
                .ForMember(dest => dest.Limit, map => map.MapFrom(source => source.PageSize));

            CreateMap<Paged<IEnumerable<EventTypeDto>>, PagedResponse<IEnumerable<EventTypeModel>>>()
                .ForMember(dest => dest.Page, map => map.MapFrom(source => source.PageNumber))
                .ForMember(dest => dest.Limit, map => map.MapFrom(source => source.PageSize));

            CreateMap<Paged<IEnumerable<FamilyDto>>, PagedResponse<IEnumerable<FamilyModel>>>()
                .ForMember(dest => dest.Page, map => map.MapFrom(source => source.PageNumber))
                .ForMember(dest => dest.Limit, map => map.MapFrom(source => source.PageSize));

            CreateMap<Paged<IEnumerable<PlaceDto>>, PagedResponse<IEnumerable<PlaceModel>>>()
                .ForMember(dest => dest.Page, map => map.MapFrom(source => source.PageNumber))
                .ForMember(dest => dest.Limit, map => map.MapFrom(source => source.PageSize));
        }
    }
}
