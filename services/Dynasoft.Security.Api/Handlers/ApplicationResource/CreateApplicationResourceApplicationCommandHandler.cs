
using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Common.Util.AutoMapper;
using Dynasoft.Security.Application.Contracts.ApplicationResource;
using Dynasoft.Security.Application.Contracts.DTOs;
using Dynasoft.Security.Domain.Entities;

using System.Threading.Tasks;

namespace Dynasoft.Security.Api.Handlers.ApplicationResource
{
    public class CreateApplicationResourceApplicationCommandHandler
        : IMessageHandler<CreateApplicationResourceApplicationCommand, ApplicationResourceDto>
    {
        private readonly ISecurityDataContext context;

        public CreateApplicationResourceApplicationCommandHandler(ISecurityDataContext context) => this.context = context;

        public async Task<ApplicationResourceDto> HandleAsync(CreateApplicationResourceApplicationCommand message)
        {
            var resource = Domain.Entities.ResourceAggregate.ApplicationResource.Create(message);
            this.context.ApplicationResources.Add(resource);

            await this.context.PersistChangesAsync();

            return resource.MapTo<ApplicationResourceDto>();
        }
    }
}
