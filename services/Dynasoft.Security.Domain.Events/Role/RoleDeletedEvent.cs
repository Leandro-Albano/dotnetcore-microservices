using Dynasoft.Common.Domain.Contracts;

namespace Dynasoft.Security.Domain.Events.Role
{
    public class RoleDeletedEvent : DomainEvent
    {
        public RoleDeletedEvent(IEventEmitter emitter) : base(emitter)
        {
        }
    }
}
