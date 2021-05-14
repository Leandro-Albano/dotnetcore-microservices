using Dynasoft.Common.Domain.Contracts;

namespace Dynasoft.Security.Domain.Entities
{
    public class AddResourceActionCommand : IDomainCommand
    {
        public string Name { get; set; }
    }
}
