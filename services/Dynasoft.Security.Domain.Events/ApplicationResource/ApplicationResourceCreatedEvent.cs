using Dynasoft.Common.Domain.Contracts;

namespace Dynasoft.Security.Domain.Events.ApplicationResource
{
    public class ApplicationResourceCreatedEvent : DomainEvent
    {
        public ApplicationResourceCreatedEvent(IEventEmitter emitter) : base(emitter)
        {
        }
    }
}
