using Dynasoft.Common.Domain.Contracts;

using System.Collections.Generic;

namespace Dynasoft.Security.Domain.Entities.UserAggregate.Commands
{
    public class CreateUserCommand : IDomainCommand
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserCommandRole> UserRoles { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string IndentityServerId { get; set; }
    }
}
