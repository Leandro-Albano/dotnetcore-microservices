using AutoMapper;

using IndeedIQ.Common.Domain.Entities.CommonQueryExtensions;
using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Security.Application.Contracts.DTOs;
using IndeedIQ.Security.Application.Contracts.User;
using IndeedIQ.Security.Domain.Entities;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndeedIQ.Security.Api.Handlers.ApplicationResource
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
