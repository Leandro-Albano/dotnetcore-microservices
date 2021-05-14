using Dynasoft.Common.Domain.Contracts.Exceptions;
using Dynasoft.Security.Domain.Contracts.Common;
using Dynasoft.Security.Domain.Entities.ResourceAggregate;
using Dynasoft.Security.Domain.Entities.RoleAggregate;
using Dynasoft.Security.Domain.Entities.RoleAggregate.Commands;
using Dynasoft.Security.Domain.Events;
using Dynasoft.Security.Domain.Events.Role;
using Dynasoft.Security.Tests.Common;

using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Dynasoft.Security.Tests.Unit.Domain.Entities.RoleAggregate
{
    public class RoleTests
    {
        public static IEnumerable<object[]> UpdatePermissionTestItems = new[]{
            new object[] { new UpdateRolePermissionsCommand { Permissions = new []{ new UpdateRolePermissionItem { } } }, new[] {
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember,
                                      Member = $"{nameof(UpdateRolePermissionsCommand.Permissions)}[0].{nameof(UpdateRolePermissionItem.Action)}" }
            } }
        };

        public static IEnumerable<object[]> CreateCmdTestItems => new[]{
            new object[]{ new CreateRoleCommand { Name = null, ApplicationLevel = ApplicationLevel.Organisation }, new [] {
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(CreateRoleCommand.Name) },
            } },
            new object[]{ new CreateRoleCommand { Name = "", ApplicationLevel = ApplicationLevel.Organisation }, new[]
            {
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(CreateRoleCommand.Name) }
            } },
            new object[]{ new CreateRoleCommand { Name = "  ", ApplicationLevel = ApplicationLevel.Organisation }, new[]
            {
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(CreateRoleCommand.Name) }
            } },
            new object[]{ new CreateRoleCommand { Name = "ValidName", ApplicationLevel = ApplicationLevel.NotSet }, new[]
            {
                new ValidationError { Code = ValidationErrorCode.InvalidValue, Member = nameof(CreateRoleCommand.ApplicationLevel) }
            } }
        };

        public static IEnumerable<object[]> UpdateCmdTestItems => new[]{
            new object[]{ new UpdateRoleCommand { Name = null, ApplicationLevel = ApplicationLevel.Organisation }, new [] {
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(UpdateRoleCommand.Name) },
            } },
            new object[]{ new UpdateRoleCommand { Name = "", ApplicationLevel = ApplicationLevel.Organisation }, new[]
            {
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(UpdateRoleCommand.Name) }
            } },
            new object[]{ new UpdateRoleCommand { Name = "  ", ApplicationLevel = ApplicationLevel.Organisation }, new[]
            {
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(UpdateRoleCommand.Name) }
            } },
            new object[]{ new UpdateRoleCommand { Name = "ValidName", ApplicationLevel = ApplicationLevel.NotSet }, new[]
            {
                new ValidationError { Code = ValidationErrorCode.InvalidValue, Member = nameof(UpdateRoleCommand.ApplicationLevel) }
            } }
        };

        [Theory]
        [MemberData(nameof(CreateCmdTestItems))]
        public void Create_ShouldThrowDomainValidationException_WhenCommandIsInvalid(CreateRoleCommand command, ValidationError[] errors)
        {
            var ex = Assert.Throws<DomainValidationException>(() => Role.Create(command));

            Assert.Equal(errors.Length, ex.ValidationErrors.Count());
            Assert.Contains(errors, item => ex.ValidationErrors.Any(err => err.Code == item.Code && err.Member == item.Member));
        }

        [Fact]
        public void Create_ShouldSetPropertyValuesCorrectly_WhenCommandIsValid()
        {
            var roleCmdGen = new RoleCmdGenerator();
            var command = roleCmdGen.CreateRoleCommand;
            var role = Role.Create(command);

            Assert.Equal(command.Name, role.Name);
        }

        [Fact]
        public void Create_ShouldAddRoleCreatedEvent_WhenCommandIsValid()
        {
            var roleCmdGen = new RoleCmdGenerator();
            var command = roleCmdGen.CreateRoleCommand;
            var role = Role.Create(command);

            Assert.Single(role.Events);
            Assert.Contains(role.Events, item => item is RoleCreatedEvent createdEvent
                                              && createdEvent.Name == command.Name);
        }

        [Theory]
        [MemberData(nameof(UpdateCmdTestItems))]
        public void Update_ShouldThrowDomainValidationException_WhenCommandIsInvalid(UpdateRoleCommand command, ValidationError[] erros)
        {
            var roleCmdGen = new RoleCmdGenerator();
            var createCmd = roleCmdGen.CreateRoleCommand;
            var role = Role.Create(createCmd);
            var ex = Assert.Throws<DomainValidationException>(() => role.Update(command));

            Assert.Equal(erros.Length, ex.ValidationErrors.Count());
            Assert.Contains(erros, item => ex.ValidationErrors.Any(err => err.Code == item.Code && err.Member == item.Member));
        }

        [Fact]
        public void Update_ShouldSetPropertyValuesCorrectly_WhenCommandIsValid()
        {
            var roleCmdGen = new RoleCmdGenerator();
            var createCmd = roleCmdGen.CreateRoleCommand;
            var role = Role.Create(createCmd);
            var updateCmd = roleCmdGen.UpdateRoleCommand;

            role.Update(updateCmd);

            Assert.Equal(updateCmd.Name, role.Name);
        }

        [Fact]
        public void Update_ShouldAddRoleUpdatedEvent_WhenCommandIsValid()
        {
            var roleCmdGen = new RoleCmdGenerator();
            var createCmd = roleCmdGen.CreateRoleCommand;
            var role = Role.Create(createCmd);
            var updateCmd = roleCmdGen.UpdateRoleCommand;

            role.Update(updateCmd);

            Assert.Equal(2, role.Events.Count);
            Assert.Contains(role.Events.Skip(1), item => item is RoleUpdatedEvent createdEvent
                                              && createdEvent.Name == updateCmd.Name);
        }

        [Theory]
        [MemberData(nameof(UpdatePermissionTestItems))]
        public void UpdatePermissions_ShouldThrowDomainValidaitonException_WhenCommandIsInvalid(UpdateRolePermissionsCommand command, ValidationError[] errors)
        {
            var roleCmdGen = new RoleCmdGenerator();
            var createCmd = roleCmdGen.CreateRoleCommand;
            var role = Role.Create(createCmd);
            var createResourceCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var resource = ApplicationResource.Create(createResourceCmd);
            var action = resource.AddResourceAction(ApplicationResourceCmdGenerator.AddResourceActionCommand);

            var ex = Assert.Throws<DomainValidationException>(() => role.UpdatePermissions(command));

            Assert.Equal(errors.Length, ex.ValidationErrors.Count());
            Assert.All(ex.ValidationErrors, e => errors.Any(err => err.Code == e.Code && err.Member == e.Member));
        }

        [Fact]
        public void UpdatePermissions_ShouldUpdatePermissions_WhenCommandIsValid()
        {
            var roleCmdGen = new RoleCmdGenerator();
            var createCmd = roleCmdGen.CreateRoleCommand;
            var role = Role.Create(createCmd);
            var createResourceCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var resource = ApplicationResource.Create(createResourceCmd);
            var action = resource.AddResourceAction(ApplicationResourceCmdGenerator.AddResourceActionCommand);
            var command = new UpdateRolePermissionsCommand
            {
                Permissions = new[] { new UpdateRolePermissionItem { Action = action, Permission = Permission.Allow } }
            };
            try
            {
                role.UpdatePermissions(command);
            }
            catch (DomainValidationException ex)
            {
                Assert.True(false, Newtonsoft.Json.JsonConvert.SerializeObject(ex.ValidationErrors));
            }

            Assert.Contains(role.RolePermissions,
                       p => p.Action == command.Permissions.Single().Action
                         && p.Permission == command.Permissions.Single().Permission);
        }

        [Fact]
        public void UpdatePermissions_ShouldAddRolePermissionsUpdatedEvent_WhenCommandIsValid()
        {
            var roleCmdGen = new RoleCmdGenerator();
            var createCmd = roleCmdGen.CreateRoleCommand;
            var role = Role.Create(createCmd);
            var createResourceCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var resource = ApplicationResource.Create(createResourceCmd);
            var action = resource.AddResourceAction(ApplicationResourceCmdGenerator.AddResourceActionCommand);
            var addPermissionCmd = roleCmdGen.UpdateRolePermissionsCommand;
            addPermissionCmd.Permissions.First().Action = action;

            try
            {
                role.UpdatePermissions(addPermissionCmd);
            }
            catch (DomainValidationException ex)
            {
                Assert.True(false, Newtonsoft.Json.JsonConvert.SerializeObject(ex.ValidationErrors));
            }

            Assert.Equal(2, role.Events.Count());

            var addedEvent = Assert.IsType<RolePermissionsUpdatedEvent>(role.Events.Last());

            Assert.Equal(role.RolePermissions.Count(), addedEvent.Permissions.Count());
            Assert.All(role.RolePermissions, (item)
                => Assert.Contains(addedEvent.Permissions, evi => evi.Action.Id == item.Action.Id && evi.Permission == item.Permission));
        }

        [Fact]
        public void Delete_ShouldAddRoleDeletedEvent_Called()
        {
            var roleCmdGen = new RoleCmdGenerator();
            var createCmd = roleCmdGen.CreateRoleCommand;
            var role = Role.Create(createCmd);
            role.Delete();

            Assert.Equal(2, role.Events.Count());
            Assert.IsType<RoleDeletedEvent>(role.Events.Last());
        }
    }
}
