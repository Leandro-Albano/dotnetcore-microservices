using Dynasoft.Common.Domain.Contracts;

namespace Dynasoft.Security.Domain.Entities.ResourceAggregate.Commands
{
    public class UpdateResourceActionCommand : IDomainCommand
    {
        public ResourceAction Action { get; set; }
        public string Name { get; set; }
    }
}
