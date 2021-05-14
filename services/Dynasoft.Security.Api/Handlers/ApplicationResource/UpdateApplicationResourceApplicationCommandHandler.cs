using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Application.Contracts.ApplicationResource;
using Dynasoft.Security.Domain.Entities;
using Dynasoft.Security.Domain.Entities.ResourceAggregate.Commands;

using Newtonsoft.Json;

using System.Threading.Tasks;

namespace Dynasoft.Security.Api.Handlers.ApplicationResource
{
    public class UpdateApplicationResourceApplicationCommandHandler : IMessageHandler<UpdateApplicationResourceApplicationCommand>
    {
        private readonly ISecurityDataContext context;

        public UpdateApplicationResourceApplicationCommandHandler(ISecurityDataContext context) => this.context = context;

        public async Task HandleAsync(UpdateApplicationResourceApplicationCommand message)
        {
            var appResource = await this.context.ApplicationResources.FindByIdAsync(message.Id, true);
            var command = new UpdateApplicationResourceCommand
            {
                ApplicationLevel = appResource.ApplicationLevel,
                Name = appResource.Name
            };

            JsonConvert.PopulateObject(System.Text.Json.JsonSerializer.Serialize(message), command);
            appResource.Update(command);

            await this.context.PersistChangesAsync();
        }
    }
}
