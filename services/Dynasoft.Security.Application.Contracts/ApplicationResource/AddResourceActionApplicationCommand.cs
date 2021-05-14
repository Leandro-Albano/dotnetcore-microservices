using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Domain.Entities;

namespace Dynasoft.Security.Application.Contracts.ApplicationResource
{
    public class AddResourceActionApplicationCommand : AddResourceActionCommand, IMessage<long>
    {
        public long ApplicationResourceId { get; set; }
    }
}
