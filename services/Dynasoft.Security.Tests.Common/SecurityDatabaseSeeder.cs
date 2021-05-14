using Dynasoft.Security.Domain.Contracts.Common;
using Dynasoft.Security.Domain.Entities;
using Dynasoft.Security.Domain.Entities.ResourceAggregate;
using Dynasoft.Security.Domain.Entities.RoleAggregate;
using Dynasoft.Security.Domain.Entities.UserAggregate;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dynasoft.Security.Tests.Common
{
    public class SecurityDatabaseSeeder
    {
        private readonly ISecurityDataContext context;

        public SecurityDatabaseSeeder(ISecurityDataContext context) => this.context = context;

        public void Seed()
        {
            this.SeedApplicationResources().ConfigureAwait(false).GetAwaiter().GetResult();
            this.SeedRoles().ConfigureAwait(false).GetAwaiter().GetResult();
            this.SeedUsers().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private async Task SeedApplicationResources()
        {
            // Application Resources
            var appResourceCreateCmd1 = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var appResourceCreateCmd2 = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var appResourceCreateCmd3 = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var appResource1 = ApplicationResource.Create(appResourceCreateCmd1);
            var appResource2 = ApplicationResource.Create(appResourceCreateCmd2);
            var appResource3 = ApplicationResource.Create(appResourceCreateCmd3);

            // Actions
            var action11 = ApplicationResourceCmdGenerator.AddResourceActionCommand;
            action11.Name = new Random().Next().ToString();
            var action12 = ApplicationResourceCmdGenerator.AddResourceActionCommand;
            appResource1.AddResourceAction(action11);
            appResource1.AddResourceAction(action12);

            var action21 = ApplicationResourceCmdGenerator.AddResourceActionCommand;
            var action22 = ApplicationResourceCmdGenerator.AddResourceActionCommand;
            appResource2.AddResourceAction(action21);
            appResource2.AddResourceAction(action22);

            var action31 = ApplicationResourceCmdGenerator.AddResourceActionCommand;
            var action32 = ApplicationResourceCmdGenerator.AddResourceActionCommand;
            appResource3.AddResourceAction(action31);
            appResource3.AddResourceAction(action32);

            this.context.ApplicationResources.Add(appResource1);
            this.context.ApplicationResources.Add(appResource2);
            this.context.ApplicationResources.Add(appResource3);

            await this.context.PersistChangesAsync();

        }

        private async Task SeedRoles()
        {
            var roleCmdGen = new RoleCmdGenerator();
            // Roles
            var roleCmd1 = roleCmdGen.CreateRoleCommand;
            roleCmd1.ApplicationLevel = ApplicationLevel.Application;

            var roleCmd2 = roleCmdGen.CreateRoleCommand;
            roleCmd2.ApplicationLevel = ApplicationLevel.Application;

            var roleCmd3 = roleCmdGen.CreateRoleCommand;
            roleCmd3.ApplicationLevel = ApplicationLevel.Application;

            var role1 = Role.Create(roleCmd1);
            var role2 = Role.Create(roleCmd2);
            var role3 = Role.Create(roleCmd3);

            // Role permissions
            var action = this.context.ApplicationResources.Include(c => c.AvailableActions).First().AvailableActions.First();
            var permissions1 = roleCmdGen.UpdateRolePermissionsCommand;
            permissions1.Permissions.Single().Action = action;
            role1.UpdatePermissions(permissions1);

            var permissions2 = roleCmdGen.UpdateRolePermissionsCommand;
            permissions2.Permissions.Single().Action = action;
            role2.UpdatePermissions(permissions2);

            var permissions3 = roleCmdGen.UpdateRolePermissionsCommand;
            permissions3.Permissions.Single().Action = action;
            role3.UpdatePermissions(permissions3);

            this.context.Roles.Add(role1);
            this.context.Roles.Add(role2);
            this.context.Roles.Add(role3);

            await this.context.PersistChangesAsync();
        }

        private async Task SeedUsers()
        {
            var userCmdGen = new UserCmdGenerator();
            var createUserCmd = userCmdGen.CreateUserCommand;
            createUserCmd.IndentityServerId = "auth0|5dcbd929b762520e480c712f";
            createUserCmd.UserRoles = this.context.Roles.ToArray().Select(r => new Dynasoft.Security.Domain.Entities.UserAggregate.Commands.UserCommandRole
            {
                GrantedAccounts = new[] { 1L, 2L, 3L },
                GrantedOrganisations = new[] { 1L, 2L, 3L },
                Role = r
            });
            var user = User.Create(createUserCmd);

            this.context.Users.Add(user);

            await this.context.PersistChangesAsync();
        }
    }
}
