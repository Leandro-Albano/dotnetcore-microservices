using IndeedIQ.Common.Domain.Contracts;

namespace IndeedIQ.Security.Domain.Entities
{
    public class AddResourceActionCommand : IDomainCommand
    {
        public string Name { get; set; }
    }
}
