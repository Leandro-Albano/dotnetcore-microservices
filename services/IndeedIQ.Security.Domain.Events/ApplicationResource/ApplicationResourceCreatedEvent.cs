using IndeedIQ.Common.Domain.Contracts;

namespace IndeedIQ.Security.Domain.Events.ApplicationResource
{
    public class ApplicationResourceCreatedEvent : DomainEvent
    {
        public ApplicationResourceCreatedEvent(IEventEmitter emitter) : base(emitter)
        {
        }
    }
}
