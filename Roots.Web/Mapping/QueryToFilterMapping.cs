using AutoMapper;
using GenDateTools;
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

            CreateMap<EventQuery, EventFilter>()
                .ForMember(dest => dest.PageNumber, map => map.MapFrom(source => source.Page))
                .ForMember(dest => dest.PageSize, map => map.MapFrom(source => source.Limit))
                .ForMember(dest => dest.DateFrom, map => map.MapFrom(source => ConvertDatePartToGenDate(source.DateFrom)))
                .ForMember(dest => dest.DateTo, map => map.MapFrom(source => ConvertDatePartToGenDate(source.DateTo)));
        }

        private static GenDate ConvertDatePartToGenDate(string value)
        {
            return value != null ? new GenDate(new DatePart(value.PadRight(8, '0'))) : null;
        }
    }
}
