using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Security.Application.Contracts.DTOs;
using IndeedIQ.Security.Domain.Entities.ResourceAggregate.Commands;

namespace IndeedIQ.Security.Application.Contracts.ApplicationResource
{
    public class CreateApplicationResourceApplicationCommand : CreateApplicationResourceCommand, IMessage<ApplicationResourceDto>
    {
    }
}
