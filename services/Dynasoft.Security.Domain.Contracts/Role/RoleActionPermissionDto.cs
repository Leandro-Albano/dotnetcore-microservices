using Dynasoft.Security.Domain.Contracts.ApplicationResource;
using Dynasoft.Security.Domain.Contracts.Common;

namespace Dynasoft.Security.Domain.Contracts.Role
{
    public class RoleActionPermissionDto
    {
        public ResourceActionDto Action { get; set; }

        public Permission Permission { get; set; }
    }
}
