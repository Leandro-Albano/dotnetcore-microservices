using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Security.Application.Contracts.DTOs;

using System.Collections.Generic;

namespace IndeedIQ.Security.Application.Contracts.Role
{
    public class SearchRolesApplicationCommand : IMessage<IEnumerable<RoleDto>>
    {
        public long[] Ids { get; set; }
        public string[] Names { get; set; }
        public bool IncludePermissions { get; set; }
    }
}
