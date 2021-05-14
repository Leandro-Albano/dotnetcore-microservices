using Dynasoft.Common.Infrastructure.Messaging.Mediator;

namespace Dynasoft.Security.Application.Contracts.Role
{
    public class DeleteRoleApplicationCommand : IMessage
    {
        public long Id { get; set; }
    }
}
