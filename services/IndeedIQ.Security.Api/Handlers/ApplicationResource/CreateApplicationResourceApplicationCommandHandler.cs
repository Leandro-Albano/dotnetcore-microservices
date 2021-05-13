
using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Common.Util.AutoMapper;
using IndeedIQ.Security.Application.Contracts.ApplicationResource;
using IndeedIQ.Security.Application.Contracts.DTOs;
using IndeedIQ.Security.Domain.Entities;

using System.Threading.Tasks;

namespace IndeedIQ.Security.Api.Handlers.ApplicationResource
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
