using AutoMapper;

using IndeedIQ.Common.Domain.Entities.CommonQueryExtensions;
using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Security.Application.Contracts.DTOs;
using IndeedIQ.Security.Application.Contracts.Role;
using IndeedIQ.Security.Domain.Entities;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndeedIQ.Security.Api.Handlers.ApplicationResource
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
