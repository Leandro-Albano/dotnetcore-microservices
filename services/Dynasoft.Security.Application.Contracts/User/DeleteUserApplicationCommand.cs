using Dynasoft.Common.Infrastructure.Messaging.Mediator;

namespace Dynasoft.Security.Application.Contracts.User
{
    public class DeleteUserApplicationCommand : IMessage
    {
        public long Id { get; set; }
    }
}
