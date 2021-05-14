using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Domain.Contracts.Common;

using System.Collections.Generic;

namespace Dynasoft.Security.Application.Contracts.Role
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
