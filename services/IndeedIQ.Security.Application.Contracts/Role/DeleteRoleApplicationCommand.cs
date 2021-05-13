using IndeedIQ.Common.Infrastructure.Messaging.Mediator;

namespace IndeedIQ.Security.Application.Contracts.Role
{
    public class DeleteRoleApplicationCommand : IMessage
    {
        public long Id { get; set; }
    }
}
