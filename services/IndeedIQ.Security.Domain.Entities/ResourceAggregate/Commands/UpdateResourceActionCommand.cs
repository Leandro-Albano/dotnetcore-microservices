using IndeedIQ.Common.Domain.Contracts;

namespace IndeedIQ.Security.Domain.Entities.ResourceAggregate.Commands
{
    public class UpdateResourceActionCommand : IDomainCommand
    {
        public ResourceAction Action { get; set; }
        public string Name { get; set; }
    }
}
