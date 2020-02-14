using AutoMapper;
using Roots.Business.Requests;
using Roots.Web.InputModels;

namespace Roots.Web.Mapping
{
    public class ModelToRequestMappingProfile : Profile
    {
        public ModelToRequestMappingProfile()
        {
            CreateMap<EventTypeCreateModel, EventTypeCreateRequest>();
            CreateMap<EventTypeUpdateModel, EventTypeUpdateRequest>();

            CreateMap<PlaceCreateModel, PlaceCreateRequest>();
            CreateMap<PlaceUpdateModel, PlaceUpdateRequest>();

            CreateMap<DeleteModel, DeleteRequest>();
        }
    }
}
