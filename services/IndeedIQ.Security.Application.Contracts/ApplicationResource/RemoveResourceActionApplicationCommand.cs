using IndeedIQ.Common.Infrastructure.Messaging.Mediator;

namespace IndeedIQ.Security.Application.Contracts.ApplicationResource
{
    public class RemoveResourceActionApplicationCommand : IMessage
    {
        public long ResourceActionId { get; set; }
        public long ApplicationResourceId { get; set; }
    }
}
