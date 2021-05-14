
using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Application.Contracts.ApplicationResource;
using Dynasoft.Security.Domain.Entities;

using System.Threading.Tasks;

namespace Dynasoft.Security.Api.Handlers.ApplicationResource
{
    public class DeleteApplicationResourceApplicationCommandHandler : IMessageHandler<DeleteApplicationResourceCommand>
    {
        private readonly ISecurityDataContext context;

        public DeleteApplicationResourceApplicationCommandHandler(ISecurityDataContext context) => this.context = context;

        public async Task HandleAsync(DeleteApplicationResourceCommand message)
        {
            var resource = await this.context.ApplicationResources.FindByIdAsync(message.Id, true);
            await this.context.ApplicationResources.DeleteAndPersistAsync(resource);
        }
    }
}
