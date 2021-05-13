using IndeedIQ.Common.Domain.Contracts;

namespace IndeedIQ.Security.Domain.Events.ApplicationResource
{
    public class ApplicationResourceActionUpdatedEvent : DomainEvent
    {
        public ApplicationResourceActionUpdatedEvent(IEventEmitter emitter) : base(emitter)
        {
        }

        public long ResourceActionId { get; set; }
        public string Name { get; set; }
    }
}
