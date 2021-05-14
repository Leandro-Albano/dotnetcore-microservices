using Dynasoft.Common.Domain.Contracts;

namespace Dynasoft.Security.Domain.Events.ApplicationResource
{
    public class NewResourceActionAddedEvent : DomainEvent
    {
        public NewResourceActionAddedEvent(IEventEmitter emitter) : base(emitter)
        {
        }

        public string Name { get; set; }
    }
}
