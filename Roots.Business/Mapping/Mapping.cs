using AutoMapper;
using System;

namespace Roots.Business.Mappings
{
    public static class Mapping
    {
        private static readonly Lazy<IMapper> _lazyMapper = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<DomainToDtoMappingProfile>();
            });
            return config.CreateMapper();
        });

        public static IMapper Mapper => _lazyMapper.Value;
    }
}
