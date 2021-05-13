using IndeedIQ.Common.Domain.Entities.CommonQueryExtensions;
using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Security.Application.Contracts.ApplicationResource;
using IndeedIQ.Security.Domain.Entities;

using System.Threading.Tasks;

namespace IndeedIQ.Security.Api.Handlers.ApplicationResource
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
