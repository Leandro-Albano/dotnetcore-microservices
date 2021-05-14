using Dynasoft.Common.Infrastructure.Messaging.Mediator;

namespace Dynasoft.Security.Application.Contracts.ApplicationResource
{
    public class RemoveResourceActionApplicationCommand : IMessage
    {
        public long ResourceActionId { get; set; }
        public long ApplicationResourceId { get; set; }
    }
}
