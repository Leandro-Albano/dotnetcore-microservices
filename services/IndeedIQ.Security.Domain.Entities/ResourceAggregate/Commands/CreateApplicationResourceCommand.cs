
using IndeedIQ.Common.Domain.Contracts;
using IndeedIQ.Security.Domain.Contracts.Common;

namespace IndeedIQ.Security.Domain.Entities.ResourceAggregate.Commands
{
    public class CreateApplicationResourceCommand : IDomainCommand
    {
        public string Name { get; set; }
        public ApplicationLevel ApplicationLevel { get; set; }
    }
}
