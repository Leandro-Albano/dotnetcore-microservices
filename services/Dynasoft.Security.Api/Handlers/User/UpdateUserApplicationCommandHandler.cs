
using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Application.Contracts.User;
using Dynasoft.Security.Domain.Entities;

using Newtonsoft.Json;

using System.Threading.Tasks;

namespace Dynasoft.Security.Api.Handlers.Role
{
    public class UpdateUserApplicationCommandHandler : IMessageHandler<UpdateUserApplicationCommand>
    {
        private readonly ISecurityDataContext context;

        public UpdateUserApplicationCommandHandler(ISecurityDataContext context) => this.context = context;

        public async Task HandleAsync(UpdateUserApplicationCommand message)
        {
            var user = await this.context.Users.FindByIdAsync(message.Id);
            var command = new Domain.Entities.UserAggregate.Commands.UpdateUserCommand();
            JsonConvert.PopulateObject(System.Text.Json.JsonSerializer.Serialize(message.ExtraProperties), command);
            user.Update(command);

            await this.context.PersistChangesAsync();
        }
    }
}
