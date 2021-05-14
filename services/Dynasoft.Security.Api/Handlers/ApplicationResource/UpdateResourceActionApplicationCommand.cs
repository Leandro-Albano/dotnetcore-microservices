using Dynasoft.Common.Domain.Entities.CommonQueryExtensions;
using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Application.Contracts.ApplicationResource;
using Dynasoft.Security.Domain.Entities;

using System.Threading.Tasks;

namespace Dynasoft.Security.Api.Handlers.ApplicationResource
{
    public class UpdateResourceActionApplicationCommandHandler : IMessageHandler<UpdateResourceActionApplicationCommand>
    {
        private readonly ISecurityDataContext context;

        public UpdateResourceActionApplicationCommandHandler(ISecurityDataContext context) => this.context = context;

        public async Task HandleAsync(UpdateResourceActionApplicationCommand message)
        {
            var appResource = await this.context.ApplicationResources.FindByIdAsync(message.ApplicationResourceId, true, c => c.AvailableActions);
            var action = appResource.AvailableActions.FindById(message.ResourceActionId, true);

            appResource.UpdateAction(new Domain.Entities.ResourceAggregate.Commands.UpdateResourceActionCommand
            {
                Action = action,
                Name = message.Name
            });

            await this.context.PersistChangesAsync();
        }
    }
}
