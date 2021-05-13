using IndeedIQ.Security.Domain.Contracts.Common;

using System.Collections.Generic;

namespace IndeedIQ.Security.Application.Contracts.DTOs
{
    public class RoleDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ApplicationLevel ApplicationLevel { get; set; }

        public IEnumerable<RoleActionPermissionDto> RolePermissions { get; set; }
    }
}
