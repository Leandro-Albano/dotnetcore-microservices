using IndeedIQ.Common.Domain.Contracts;

namespace IndeedIQ.Security.Domain.Events.ApplicationResource
{
    public class NewResourceActionAddedEvent : DomainEvent
    {
        public NewResourceActionAddedEvent(IEventEmitter emitter) : base(emitter)
        {
        }

        public string Name { get; set; }
    }
}
