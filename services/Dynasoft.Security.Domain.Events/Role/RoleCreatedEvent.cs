using Dynasoft.Common.Domain.Contracts;

namespace Dynasoft.Security.Domain.Events.Role
{
    public class RoleCreatedEvent : DomainEvent
    {
        public RoleCreatedEvent(IEventEmitter emitter) : base(emitter)
        {
        }

        public string Name { get; set; }
    }
}
