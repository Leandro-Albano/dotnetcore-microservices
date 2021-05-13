using IndeedIQ.Common.Domain.Contracts;
using IndeedIQ.Security.Domain.Contracts.Common;
using IndeedIQ.Security.Domain.Entities.ResourceAggregate;

using System.Collections.Generic;

namespace IndeedIQ.Security.Domain.Entities.RoleAggregate.Commands
{
    public class UpdateRolePermissionsCommand : IDomainCommand
    {
        public IEnumerable<UpdateRolePermissionItem> Permissions { get; set; }
    }

    public class UpdateRolePermissionItem
    {
        public ResourceAction Action { get; set; }
        public Permission Permission { get; set; }

    }
}
