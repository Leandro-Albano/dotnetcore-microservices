
using Dynasoft.Common.Domain.Contracts;
using Dynasoft.Security.Domain.Contracts.Common;

namespace Dynasoft.Security.Domain.Entities.ResourceAggregate.Commands
{
    public class CreateApplicationResourceCommand : IDomainCommand
    {
        public string Name { get; set; }
        public ApplicationLevel ApplicationLevel { get; set; }
    }
}
