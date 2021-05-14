using Dynasoft.Common.Domain.Contracts;
using Dynasoft.Security.Domain.Contracts.Role;

using System.Collections.Generic;

namespace Dynasoft.Security.Domain.Events
{
    public class RolePermissionsUpdatedEvent : DomainEvent
    {
        public RolePermissionsUpdatedEvent(IEventEmitter emitter) : base(emitter)
        {
        }

        public IEnumerable<RoleActionPermissionDto> Permissions { get; set; }
    }
}
