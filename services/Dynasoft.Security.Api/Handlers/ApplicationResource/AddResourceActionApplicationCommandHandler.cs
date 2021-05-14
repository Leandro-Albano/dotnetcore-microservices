using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Application.Contracts.ApplicationResource;
using Dynasoft.Security.Domain.Entities;

using System.Threading.Tasks;

namespace Dynasoft.Security.Api.Handlers.ApplicationResource
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
