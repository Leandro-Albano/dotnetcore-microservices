using Dynasoft.Common.Domain.Contracts;
using Dynasoft.Security.Domain.Contracts.Common;

namespace Dynasoft.Security.Domain.Events.ApplicationResource
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
