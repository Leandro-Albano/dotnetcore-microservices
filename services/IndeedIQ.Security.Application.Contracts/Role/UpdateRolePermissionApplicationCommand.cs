using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Security.Domain.Contracts.Common;

using System.Collections.Generic;

namespace IndeedIQ.Security.Application.Contracts.Role
{
    public class UpdateRolePermissionApplicationCommand : IMessage
    {
        public long RoleId { get; set; }
        public IEnumerable<PermissionItemApplicationCommandMember> Permissions { get; set; }
    }

    public class PermissionItemApplicationCommandMember
    {
        public long ActionId { get; set; }
        public Permission Permission { get; set; }
    }
}
