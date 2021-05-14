using Dynasoft.Security.Domain.Contracts.Common;

using System.Collections.Generic;

namespace Dynasoft.Security.Application.Contracts.DTOs
{
    public class RoleDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ApplicationLevel ApplicationLevel { get; set; }

        public IEnumerable<RoleActionPermissionDto> RolePermissions { get; set; }
    }
}
