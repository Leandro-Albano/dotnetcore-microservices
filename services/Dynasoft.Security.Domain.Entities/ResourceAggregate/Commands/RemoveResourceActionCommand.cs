using Dynasoft.Common.Domain.Contracts;

namespace Dynasoft.Security.Domain.Entities.ResourceAggregate.Commands
{
    public class RemoveResourceActionCommand : IDomainCommand
    {
        public ResourceAction Action { get; set; }
    }
}
