using IndeedIQ.Common.Domain.Entities;
using IndeedIQ.Security.Domain.Entities.UserAggregate.Commands;

using System.Collections.Generic;
using System.Linq;

namespace IndeedIQ.Security.Domain.Entities.UserAggregate
{
    public class User : AggregateRoot<User>
    {
        public string Name { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }

        private List<UserRole> roles = new List<UserRole>();
        public IReadOnlyCollection<UserRole> Roles => this.roles.AsReadOnly();

        public string IdentityServerId { get; internal set; }

        public static User Create(CreateUserCommand command)
        {
            command.Validate(true);

            var user = new User
            {
                Name = command.Name,
                Login = command.Login,
                Email = command.Email,
                Country = command.Country,
                Currency = command.Currency,
                IdentityServerId = command.IndentityServerId,
                roles = command.UserRoles.Select(r => new UserRole
                {
                    Role = r.Role,
                    Accounts = r.GrantedAccounts?.ToList(),
                    Organisations = r.GrantedOrganisations?.ToList()
                }).ToList()
            };

            return user;
        }

        public void Update(UpdateUserCommand command)
        {
        }

        public override string ToString() => $"{nameof(User)}: {this.Id}";
    }
}
