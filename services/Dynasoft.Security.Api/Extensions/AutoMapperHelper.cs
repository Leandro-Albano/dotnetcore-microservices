using AutoMapper;

using Dynasoft.Security.Application.Contracts.DTOs;
using Dynasoft.Security.Domain.Entities.ResourceAggregate;
using Dynasoft.Security.Domain.Entities.RoleAggregate;
using Dynasoft.Security.Domain.Entities.UserAggregate;

namespace Dynasoft.Security.Api.Extensions
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
