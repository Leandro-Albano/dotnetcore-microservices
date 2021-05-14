using Dynasoft.Common.Infrastructure.Messaging.Mediator;

namespace Dynasoft.Security.Application.Contracts.ApplicationResource
{
    public class DeleteApplicationResourceCommand : IMessage
    {
        public long Id { get; set; }
    }
}
