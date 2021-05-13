using IndeedIQ.Common.Infrastructure.Messaging.Mediator;

namespace IndeedIQ.Security.Application.Contracts.User
{
    public class DeleteUserApplicationCommand : IMessage
    {
        public long Id { get; set; }
    }
}
