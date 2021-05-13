
using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Security.Application.Contracts.Role;
using IndeedIQ.Security.Domain.Entities;

using System.Threading.Tasks;

namespace IndeedIQ.Security.Api.Handlers.Role
{
    public class DeleteRoleApplicationCommandHandler : IMessageHandler<DeleteRoleApplicationCommand>
    {
        private readonly ISecurityDataContext context;

        public DeleteRoleApplicationCommandHandler(ISecurityDataContext context) => this.context = context;

        public async Task HandleAsync(DeleteRoleApplicationCommand message)
        {
            var role = await this.context.Roles.FindByIdAsync(message.Id);

            await this.context.Roles.DeleteAndPersistAsync(role);
        }
    }
}
