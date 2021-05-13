using IndeedIQ.Security.Domain.Contracts.ApplicationResource;
using IndeedIQ.Security.Domain.Contracts.Common;

namespace IndeedIQ.Security.Domain.Contracts.Role
{
    public class RoleActionPermissionDto
    {
        public ResourceActionDto Action { get; set; }

        public Permission Permission { get; set; }
    }
}
