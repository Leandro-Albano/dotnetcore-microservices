using Dynasoft.Common.Infrastructure.Messaging.Mediator;

namespace Dynasoft.Security.Application.Contracts.Role
{
    public class RemoveRolePermissionApplicationCommand : IMessage
    {
        public long ResourceActionId { get; set; }
        public long RoleId { get; set; }
    }
}
