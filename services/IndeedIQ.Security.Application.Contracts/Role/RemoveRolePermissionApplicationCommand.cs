using IndeedIQ.Common.Infrastructure.Messaging.Mediator;

namespace IndeedIQ.Security.Application.Contracts.Role
{
    public class RemoveRolePermissionApplicationCommand : IMessage
    {
        public long ResourceActionId { get; set; }
        public long RoleId { get; set; }
    }
}
