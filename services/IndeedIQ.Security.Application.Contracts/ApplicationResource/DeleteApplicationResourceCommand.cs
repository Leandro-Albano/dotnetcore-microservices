using IndeedIQ.Common.Infrastructure.Messaging.Mediator;

namespace IndeedIQ.Security.Application.Contracts.ApplicationResource
{
    public class DeleteApplicationResourceCommand : IMessage
    {
        public long Id { get; set; }
    }
}
