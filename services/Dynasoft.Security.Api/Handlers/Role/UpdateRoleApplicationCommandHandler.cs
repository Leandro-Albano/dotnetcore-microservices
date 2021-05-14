
using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Application.Contracts.Role;
using Dynasoft.Security.Domain.Entities;

using Newtonsoft.Json;

using System.Threading.Tasks;

namespace Dynasoft.Security.Api.Handlers.Role
{
    public class UpdateRoleApplicationCommandHandler : IMessageHandler<UpdateRoleApplicationCommand>
    {
        private readonly ISecurityDataContext context;

        public UpdateRoleApplicationCommandHandler(ISecurityDataContext context) => this.context = context;

        public async Task HandleAsync(UpdateRoleApplicationCommand message)
        {
            var role = await this.context.Roles.FindByIdAsync(message.Id);
            var command = new Domain.Entities.RoleAggregate.Commands.UpdateRoleCommand
            {
                Name = role.Name
            };
            JsonConvert.PopulateObject(System.Text.Json.JsonSerializer.Serialize(message.ExtraProperties), command);
            role.Update(command);

            await this.context.PersistChangesAsync();
        }
    }
}
