using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Security.Application.Contracts.DTOs;
using IndeedIQ.Security.Domain.Entities.RoleAggregate.Commands;

namespace IndeedIQ.Security.Application.Contracts.Role
{
    public class CreateRoleApplicationCommand : CreateRoleCommand, IMessage<RoleDto>
    {
    }
}
