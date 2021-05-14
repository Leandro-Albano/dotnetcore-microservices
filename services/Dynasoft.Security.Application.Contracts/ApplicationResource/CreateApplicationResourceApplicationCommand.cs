using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Application.Contracts.DTOs;
using Dynasoft.Security.Domain.Entities.ResourceAggregate.Commands;

namespace Dynasoft.Security.Application.Contracts.ApplicationResource
{
    public class CreateApplicationResourceApplicationCommand : CreateApplicationResourceCommand, IMessage<ApplicationResourceDto>
    {
    }
}
