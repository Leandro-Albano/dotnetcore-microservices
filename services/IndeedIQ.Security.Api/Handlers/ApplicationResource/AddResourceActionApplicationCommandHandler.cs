using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Security.Application.Contracts.ApplicationResource;
using IndeedIQ.Security.Domain.Entities;

using System.Threading.Tasks;

namespace IndeedIQ.Security.Api.Handlers.ApplicationResource
{
    public class AddResourceActionApplicationCommandHandler : IMessageHandler<AddResourceActionApplicationCommand, long>
    {
        private readonly ISecurityDataContext context;

        public AddResourceActionApplicationCommandHandler(ISecurityDataContext context) => this.context = context;

        public async Task<long> HandleAsync(AddResourceActionApplicationCommand message)
        {
            var appResource = await this.context.ApplicationResources.FindByIdAsync(message.ApplicationResourceId);

            var action = appResource.AddResourceAction(message);
            await this.context.PersistChangesAsync();

            return action.Id;
        }
    }
}
