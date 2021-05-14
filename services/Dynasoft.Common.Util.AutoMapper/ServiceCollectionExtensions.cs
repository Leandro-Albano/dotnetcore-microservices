using AutoMapper;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace Dynasoft.Common.Util.AutoMapper
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAutoMapperMappings(this IServiceCollection services, Action<IMapperConfigurationExpression> action)
        {
            MapperConfiguration config = new MapperConfiguration(cfg => action?.Invoke(cfg));

            IMapper mapper = config.CreateMapper();
            ObjectExtensions.Mapper = mapper;

            services.AddSingleton<IConfigurationProvider>(config);
            services.AddSingleton(mapper);
        }
    }
}
