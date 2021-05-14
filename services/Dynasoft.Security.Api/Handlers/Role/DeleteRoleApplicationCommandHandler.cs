
using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Application.Contracts.Role;
using Dynasoft.Security.Domain.Entities;

using System.Threading.Tasks;

namespace Dynasoft.Security.Api.Handlers.Role
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
