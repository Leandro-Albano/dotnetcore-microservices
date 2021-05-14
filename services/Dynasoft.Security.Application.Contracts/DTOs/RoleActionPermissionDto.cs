using Dynasoft.Security.Domain.Contracts.Common;

namespace Dynasoft.Security.Application.Contracts.DTOs
{
    public class RoleActionPermissionDto
    {
        public ResourceActionDto Action { get; set; }
        public Permission Permission { get; set; }
    }
}
