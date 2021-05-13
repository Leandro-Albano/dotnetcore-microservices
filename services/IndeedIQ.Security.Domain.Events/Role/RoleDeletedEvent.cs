using IndeedIQ.Common.Domain.Contracts;

namespace IndeedIQ.Security.Domain.Events.Role
{
    public class RoleDeletedEvent : DomainEvent
    {
        public RoleDeletedEvent(IEventEmitter emitter) : base(emitter)
        {
        }
    }
}
