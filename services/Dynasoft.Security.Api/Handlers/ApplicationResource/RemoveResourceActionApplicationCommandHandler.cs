using Dynasoft.Common.Domain.Entities.CommonQueryExtensions;
using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Application.Contracts.ApplicationResource;
using Dynasoft.Security.Domain.Entities;

using System.Threading.Tasks;

namespace Dynasoft.Security.Api.Handlers.ApplicationResource
{
    public class RemoveResourceActionApplicationCommandHandler : IMessageHandler<RemoveResourceActionApplicationCommand>
    {
        private readonly ISecurityDataContext context;

        public RemoveResourceActionApplicationCommandHandler(ISecurityDataContext context) => this.context = context;

        public async Task HandleAsync(RemoveResourceActionApplicationCommand message)
        {
            var resource = await this.context.ApplicationResources
                .FindByIdAsync(message.ApplicationResourceId, true, p => p.AvailableActions);

            var action = resource.AvailableActions.FindById(message.ResourceActionId, throws: true);

            resource.RemoveResourceAction(new Domain.Entities.ResourceAggregate.Commands.RemoveResourceActionCommand
            {
                Action = action
            });

            await this.context.PersistChangesAsync();
        }
    }
}
