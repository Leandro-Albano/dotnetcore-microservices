using AutoMapper;

using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Security.Application.Contracts.DTOs;
using IndeedIQ.Security.Application.Contracts.Role;
using IndeedIQ.Security.Domain.Entities;

using System.Threading.Tasks;

namespace IndeedIQ.Security.Api.Handlers.Role
{
    public class CreateRoleApplicationCommandHandler : IMessageHandler<CreateRoleApplicationCommand, RoleDto>
    {
        private readonly ISecurityDataContext context;
        private readonly IMapper mapper;

        public CreateRoleApplicationCommandHandler(ISecurityDataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<RoleDto> HandleAsync(CreateRoleApplicationCommand message)
        {
            var role = Domain.Entities.RoleAggregate.Role.Create(message);
            await this.context.Roles.AddAndPersistAsync(role);

            return this.mapper.Map<RoleDto>(role);
        }
    }
}
