using Dynasoft.Common.Domain.Contracts;

namespace Dynasoft.Security.Domain.Events
{
    public class ApplicationResourceActionRemovedEvent : DomainEvent
    {
        public ApplicationResourceActionRemovedEvent(IEventEmitter emitter) : base(emitter)
        {
        }

        public long ResourceActionId { get; set; }
    }
}
