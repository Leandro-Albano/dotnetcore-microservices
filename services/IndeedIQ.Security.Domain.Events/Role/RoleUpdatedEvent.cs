using IndeedIQ.Common.Domain.Contracts;

namespace IndeedIQ.Security.Tests.Unit.Domain.Entities.RoleAggregate
{
    public class RoleUpdatedEvent : DomainEvent
    {
        public RoleUpdatedEvent(IEventEmitter emitter) : base(emitter)
        {
        }

        public string Name { get; set; }
    }
}
