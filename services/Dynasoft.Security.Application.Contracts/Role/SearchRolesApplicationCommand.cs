using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Application.Contracts.DTOs;

using System.Collections.Generic;

namespace Dynasoft.Security.Application.Contracts.Role
{
    public class SearchRolesApplicationCommand : IMessage<IEnumerable<RoleDto>>
    {
        public long[] Ids { get; set; }
        public string[] Names { get; set; }
        public bool IncludePermissions { get; set; }
    }
}
