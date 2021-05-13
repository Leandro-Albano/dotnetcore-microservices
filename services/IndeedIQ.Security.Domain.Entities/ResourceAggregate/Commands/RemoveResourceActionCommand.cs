using IndeedIQ.Common.Domain.Contracts;

namespace IndeedIQ.Security.Domain.Entities.ResourceAggregate.Commands
{
    public class RemoveResourceActionCommand : IDomainCommand
    {
        public ResourceAction Action { get; set; }
    }
}
