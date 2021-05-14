using Dynasoft.Common.Infrastructure.Messaging.Mediator;

namespace Dynasoft.Security.Application.Contracts.ApplicationResource
{
    public class UpdateResourceActionApplicationCommand : IMessage
    {
        public long ApplicationResourceId { get; set; }
        public long ResourceActionId { get; set; }
        public string Name { get; set; }
    }
}
