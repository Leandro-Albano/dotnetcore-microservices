using Dynasoft.Common.Domain.Contracts;

namespace Dynasoft.Security.Tests.Unit.Domain.Entities.RoleAggregate
{
    public class RoleUpdatedEvent : DomainEvent
    {
        public RoleUpdatedEvent(IEventEmitter emitter) : base(emitter)
        {
        }

        public string Name { get; set; }
    }
}
