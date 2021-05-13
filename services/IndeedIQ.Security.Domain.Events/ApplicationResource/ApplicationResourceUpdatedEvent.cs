using IndeedIQ.Common.Domain.Contracts;
using IndeedIQ.Security.Domain.Contracts.Common;

namespace IndeedIQ.Security.Domain.Events.ApplicationResource
{
    public class ApplicationResourceUpdatedEvent : DomainEvent
    {
        public ApplicationResourceUpdatedEvent(IEventEmitter emitter) : base(emitter)
        {
        }

        public string Name { get; set; }
        public ApplicationLevel ApplicationLevel { get; set; }
    }
}
