using IndeedIQ.Common.Domain.Contracts;

namespace IndeedIQ.Security.Domain.Events
{
    public class ApplicationResourceActionRemovedEvent : DomainEvent
    {
        public ApplicationResourceActionRemovedEvent(IEventEmitter emitter) : base(emitter)
        {
        }

        public long ResourceActionId { get; set; }
    }
}
