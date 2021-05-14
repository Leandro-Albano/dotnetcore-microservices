using AutoMapper;

using Dynasoft.Common.Domain.Entities.CommonQueryExtensions;
using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Application.Contracts.DTOs;
using Dynasoft.Security.Application.Contracts.User;
using Dynasoft.Security.Domain.Entities;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dynasoft.Security.Api.Handlers.ApplicationResource
{
    public class SearchUserApplicationCommandHandler
        : IMessageHandler<SearchUserApplicationCommand, IEnumerable<UserDto>>
    {
        private readonly ISecurityDataContext context;
        private readonly IMapper mapper;

        public SearchUserApplicationCommandHandler(ISecurityDataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> HandleAsync(SearchUserApplicationCommand message)
        {
            var query = this.context.Users.AsReadOnly();

            if (message.IncludeRoles)
                query = query.Include(c => c.Roles).ThenInclude(c => c.Role);

            var result = query.WhereIdIn(message.Ids).ToArray();
            return this.mapper.Map<IEnumerable<UserDto>>(result);
        }
    }
}
