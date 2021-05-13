using IndeedIQ.Common.Domain.Contracts;

namespace IndeedIQ.Security.Domain.Entities
{
    public class ApplicationResourceDeletedEvent : DomainEvent
    {
        public ApplicationResourceDeletedEvent(IEventEmitter emitter) : base(emitter)
        {
        }
    }
}