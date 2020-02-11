using AutoMapper;
using Roots.Business.Filters;
using Roots.Web.Queries;

namespace Roots.Web.Mapping
{
    public class QueryToFilterMapping : Profile
    {
        public QueryToFilterMapping()
        {
            CreateMap<PersonQuery, PersonFilter>()
                .ForMember(dest => dest.PageNumber, map => map.MapFrom(source => source.Page))
                .ForMember(dest => dest.PageSize, map => map.MapFrom(source => source.Limit));
        }
    }
}
