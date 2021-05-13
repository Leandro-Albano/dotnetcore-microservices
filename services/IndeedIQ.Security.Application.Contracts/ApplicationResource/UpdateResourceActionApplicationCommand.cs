using IndeedIQ.Common.Infrastructure.Messaging.Mediator;

namespace IndeedIQ.Security.Application.Contracts.ApplicationResource
{
    public class UpdateResourceActionApplicationCommand : IMessage
    {
        public long ApplicationResourceId { get; set; }
        public long ResourceActionId { get; set; }
        public string Name { get; set; }
    }
}
