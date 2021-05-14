using Dynasoft.Common.Domain.Contracts;

namespace Dynasoft.Security.Domain.Entities
{
    public class ApplicationResourceDeletedEvent : DomainEvent
    {
        public ApplicationResourceDeletedEvent(IEventEmitter emitter) : base(emitter)
        {
        }
    }
}