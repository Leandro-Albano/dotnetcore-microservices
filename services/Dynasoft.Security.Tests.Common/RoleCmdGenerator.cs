using Bogus;

using Dynasoft.Security.Domain.Contracts.Common;
using Dynasoft.Security.Domain.Entities.ResourceAggregate;
using Dynasoft.Security.Domain.Entities.RoleAggregate.Commands;

namespace Dynasoft.Security.Tests.Common
{
    public class RoleCmdGenerator
    {
        private readonly Faker<CreateRoleCommand> createRoleCmdFaker = new Faker<CreateRoleCommand>()
            .RuleFor(o => o.Name, f => f.Commerce.Department())
            .RuleFor(o => o.ApplicationLevel, f => f.PickRandomWithout(ApplicationLevel.NotSet));
        public CreateRoleCommand CreateRoleCommand => this.createRoleCmdFaker.Generate();

        private readonly Faker<UpdateRoleCommand> updateRoleCmdFaker = new Faker<UpdateRoleCommand>()
            .RuleFor(o => o.Name, f => f.Commerce.Department())
            .RuleFor(o => o.ApplicationLevel, f => f.PickRandomWithout(ApplicationLevel.NotSet));
        public UpdateRoleCommand UpdateRoleCommand => this.updateRoleCmdFaker.Generate();

        private readonly Faker<UpdateRolePermissionsCommand> udpateRolePermissionsCmdFaker = new Faker<UpdateRolePermissionsCommand>()
            .RuleFor(o => o.Permissions, new[] { new UpdateRolePermissionItem { } });
        public UpdateRolePermissionsCommand UpdateRolePermissionsCommand => this.udpateRolePermissionsCmdFaker.Generate();

        private readonly Faker<UpdateRolePermissionItem> updateRolePermissionItemFaker = new Faker<UpdateRolePermissionItem>()
            .RuleFor(o => o.Action, default(ResourceAction))
            .RuleFor(o => o.Permission, f => f.PickRandom<Permission>());
        public UpdateRolePermissionItem UpdateRolePermissionItem => this.updateRolePermissionItemFaker.Generate();
    }
}
