using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Security.Domain.Entities;

namespace IndeedIQ.Security.Application.Contracts.ApplicationResource
{
    public class AddResourceActionApplicationCommand : AddResourceActionCommand, IMessage<long>
    {
        public long ApplicationResourceId { get; set; }
    }
}
