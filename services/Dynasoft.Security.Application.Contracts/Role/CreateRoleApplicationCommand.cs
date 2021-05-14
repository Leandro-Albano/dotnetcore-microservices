using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Application.Contracts.DTOs;
using Dynasoft.Security.Domain.Entities.RoleAggregate.Commands;

namespace Dynasoft.Security.Application.Contracts.Role
{
    public class CreateRoleApplicationCommand : CreateRoleCommand, IMessage<RoleDto>
    {
    }
}
