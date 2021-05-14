using AutoMapper;

using Dynasoft.Common.Domain.Entities.CommonQueryExtensions;
using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Application.Contracts.DTOs;
using Dynasoft.Security.Application.Contracts.Role;
using Dynasoft.Security.Domain.Entities;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dynasoft.Security.Api.Handlers.ApplicationResource
{
    public class SearchRolesApplicationCommandHandler
        : IMessageHandler<SearchRolesApplicationCommand, IEnumerable<RoleDto>>
    {
        private readonly ISecurityDataContext context;
        private readonly IMapper mapper;

        public SearchRolesApplicationCommandHandler(ISecurityDataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> HandleAsync(SearchRolesApplicationCommand message)
        {
            var query = this.context.Roles.AsReadOnly();

            if (message.IncludePermissions)
                query = query.Include(c => c.RolePermissions).ThenInclude(c => c.Action);

            var result = query.WhereIdIn(message.Ids).ToArray();
            return this.mapper.Map<IEnumerable<RoleDto>>(result);
        }
    }
}
