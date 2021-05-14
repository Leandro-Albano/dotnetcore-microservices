
using Dynasoft.Security.Domain.Entities.RoleAggregate;

using System.Collections.Generic;

namespace Dynasoft.Security.Domain.Entities.UserAggregate.Commands
{
    public class UserCommandRole
    {
        public Role Role { get; set; }
        public IEnumerable<long> GrantedAccounts { get; set; }
        public IEnumerable<long> GrantedOrganisations { get; set; }
    }
}
