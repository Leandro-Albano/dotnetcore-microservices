using IndeedIQ.Common.Domain.Contracts;
using IndeedIQ.Security.Domain.Contracts.Role;

using System.Collections.Generic;

namespace IndeedIQ.Security.Domain.Events
{
    public class RolePermissionsUpdatedEvent : DomainEvent
    {
        public RolePermissionsUpdatedEvent(IEventEmitter emitter) : base(emitter)
        {
        }

        public IEnumerable<RoleActionPermissionDto> Permissions { get; set; }
    }
}
