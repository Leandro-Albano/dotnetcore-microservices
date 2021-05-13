using IndeedIQ.Common.Domain.Contracts;

namespace IndeedIQ.Security.Domain.Events.Role
{
    public class RoleCreatedEvent : DomainEvent
    {
        public RoleCreatedEvent(IEventEmitter emitter) : base(emitter)
        {
        }

        public string Name { get; set; }
    }
}
