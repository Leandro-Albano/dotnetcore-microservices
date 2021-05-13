using IndeedIQ.Security.Domain.Contracts.Common;

namespace IndeedIQ.Security.Application.Contracts.DTOs
{
    public class RoleActionPermissionDto
    {
        public ResourceActionDto Action { get; set; }
        public Permission Permission { get; set; }
    }
}
