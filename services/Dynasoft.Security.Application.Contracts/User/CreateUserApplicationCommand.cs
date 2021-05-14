using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Application.Contracts.DTOs;

using System.Collections.Generic;

namespace Dynasoft.Security.Application.Contracts.User
{
    public class CreateUserApplicationCommand : IMessage<UserDto>
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserApplicationCommandRole> UserRoles { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
    }

    public class UserApplicationCommandRole
    {
        public long RoleId { get; set; }
        public IEnumerable<long> GrantedAccounts { get; set; }
        public IEnumerable<long> GrantedOrganisations { get; set; }
    }
}
