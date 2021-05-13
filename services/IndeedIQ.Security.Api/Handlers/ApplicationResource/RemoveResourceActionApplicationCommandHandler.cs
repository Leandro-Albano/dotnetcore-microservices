using IndeedIQ.Common.Domain.Entities.CommonQueryExtensions;
using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Security.Application.Contracts.ApplicationResource;
using IndeedIQ.Security.Domain.Entities;

using System.Threading.Tasks;

namespace IndeedIQ.Security.Api.Handlers.ApplicationResource
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
