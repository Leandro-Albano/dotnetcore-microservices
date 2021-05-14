using Dynasoft.Common.Domain.Contracts;
using Dynasoft.Security.Domain.Contracts.Common;
using Dynasoft.Security.Domain.Entities.ResourceAggregate;

using System.Collections.Generic;

namespace Dynasoft.Security.Domain.Entities.RoleAggregate.Commands
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
