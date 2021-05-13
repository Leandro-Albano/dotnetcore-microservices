using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Security.Application.Contracts.ApplicationResource;
using IndeedIQ.Security.Domain.Entities;
using IndeedIQ.Security.Domain.Entities.ResourceAggregate.Commands;

using Newtonsoft.Json;

using System.Threading.Tasks;

namespace IndeedIQ.Security.Api.Handlers.ApplicationResource
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
