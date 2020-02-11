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
            CreateMap<Paged<IEnumerable<PersonDto>>, PagedResponse<IEnumerable<PersonVm>>>()
                .ForMember(dest => dest.Page, map => map.MapFrom(source => source.PageNumber))
                .ForMember(dest => dest.Limit, map => map.MapFrom(source => source.PageSize));
        }
    }
}
