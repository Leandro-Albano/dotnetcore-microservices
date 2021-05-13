using AutoMapper;

using IndeedIQ.Security.Application.Contracts.DTOs;
using IndeedIQ.Security.Domain.Entities.ResourceAggregate;
using IndeedIQ.Security.Domain.Entities.RoleAggregate;
using IndeedIQ.Security.Domain.Entities.UserAggregate;

namespace IndeedIQ.Security.Api.Extensions
{
    public static class AutoMapperHelper
    {
        public static void AddMappings(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ApplicationResource, ApplicationResourceDto>();
            cfg.CreateMap<ResourceAction, ResourceActionDto>();
            cfg.CreateMap<Role, RoleDto>();
            cfg.CreateMap<RoleActionPermission, RoleActionPermissionDto>();
            cfg.CreateMap<User, UserDto>();
        }
    }
}
