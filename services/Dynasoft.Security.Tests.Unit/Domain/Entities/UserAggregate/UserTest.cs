using Dynasoft.Common.Domain.Contracts.Exceptions;
using Dynasoft.Security.Domain.Contracts.Common;
using Dynasoft.Security.Domain.Entities.ResourceAggregate;
using Dynasoft.Security.Domain.Entities.RoleAggregate;
using Dynasoft.Security.Domain.Entities.UserAggregate;
using Dynasoft.Security.Domain.Entities.UserAggregate.Commands;
using Dynasoft.Security.Domain.Entities.Validation;
using Dynasoft.Security.Tests.Common;

using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Dynasoft.Security.Tests.Unit.Domain.Entities.UserAggregate
{
    public class UserTest
    {
        public static IEnumerable<object[]> CreateCmdTestItems
        {
            get
            {
                var userCmdGen = new UserCmdGenerator();
                var cmdWithEmptyRoles = userCmdGen.CreateUserCommand;
                cmdWithEmptyRoles.UserRoles = Enumerable.Empty<UserCommandRole>();

                var cmdWithNullUserRoleItem = userCmdGen.CreateUserCommand;
                cmdWithNullUserRoleItem.UserRoles = new UserCommandRole[] { null };

                var cmdWithNullUserRoleItemRole = userCmdGen.CreateUserCommand;
                cmdWithNullUserRoleItemRole.UserRoles = new[] { new UserCommandRole { } };

                return new[]{
                    new object[]{ new CreateUserCommand {  }, new [] {
                        new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(CreateUserCommand.Name) },
                        new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(CreateUserCommand.Login) },
                        new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(CreateUserCommand.Email) },
                        new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(CreateUserCommand.Country) },
                        new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(CreateUserCommand.Currency) },
                        new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(CreateUserCommand.UserRoles) },
                    } },
                    new object[]{ cmdWithEmptyRoles, new [] {
                        new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(CreateUserCommand.UserRoles) },
                    } },
                    new object[]{ cmdWithNullUserRoleItem, new [] {
                        new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = $"{nameof(CreateUserCommand.UserRoles)}[0]" },
                    } },
                    new object[]{ cmdWithNullUserRoleItemRole, new [] {
                        new ValidationError {
                            Code = ValidationErrorCode.MissingRequiredMember,
                            Member = $"{nameof(CreateUserCommand.UserRoles)}[0].{nameof(UserCommandRole.Role)}"
                        },
                    } },
                };
            }
        }

        [Theory]
        [MemberData(nameof(CreateCmdTestItems))]
        public void Create_ShouldThrowDomainValidationException_WhenCommandIsInvalid(CreateUserCommand command, ValidationError[] errors)
        {
            var ex = Assert.Throws<DomainValidationException>(() => User.Create(command));
            Assert.Equal(errors.Count(), ex.ValidationErrors.Count());
            Assert.All(ex.ValidationErrors, err => Assert.Contains(errors, item => err.Code == item.Code && err.Member == item.Member));
        }

        [Theory]
        [InlineData(ApplicationLevel.Account, SecurityDomainValidationErrorCode.NoGrantedAccountsProvided)]
        [InlineData(ApplicationLevel.Organisation, SecurityDomainValidationErrorCode.NoGrantedOrganisationsProvided)]
        public void Create_ShouldThrowDomainValidationException_WhenRoleLevelIsAccountAndNoAccountIdsProvided(ApplicationLevel level, string errorCode)
        {
            var createAppResourceCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            createAppResourceCmd.ApplicationLevel = level;
            var appResource = ApplicationResource.Create(createAppResourceCmd);

            var addActionCmd = ApplicationResourceCmdGenerator.AddResourceActionCommand;
            var action = appResource.AddResourceAction(addActionCmd);

            var roleCmdGen = new RoleCmdGenerator();
            var createRoleCmd = roleCmdGen.CreateRoleCommand;
            createRoleCmd.ApplicationLevel = level;
            var role = Role.Create(createRoleCmd);
            var updateRolePermissions = roleCmdGen.UpdateRolePermissionsCommand;
            updateRolePermissions.Permissions.First().Action = action;

            role.UpdatePermissions(updateRolePermissions);

            var userCmdGen = new UserCmdGenerator();
            var createUserCmd = userCmdGen.CreateUserCommand;
            createUserCmd.UserRoles = new[] { new UserCommandRole { Role = role } };

            var ex = Assert.Throws<DomainValidationException>(() => User.Create(createUserCmd));
            Assert.Single(ex.ValidationErrors);
            Assert.Equal(errorCode, ex.ValidationErrors.Single().Code);
        }

        [Fact]
        public void Create_ShouldSetAllPropertiesCorrectly_WhenCommandIsValid()
        {
            var roleCmdGen = new RoleCmdGenerator();
            var createRoleCmd = roleCmdGen.CreateRoleCommand;
            createRoleCmd.ApplicationLevel = ApplicationLevel.Organisation;

            var roleOrgLevel = Role.Create(createRoleCmd);
            createRoleCmd.ApplicationLevel = ApplicationLevel.Account;
            var roleAccLevel = Role.Create(createRoleCmd);

            var userCmdGen = new UserCmdGenerator();
            var createUserCmd = userCmdGen.CreateUserCommand;
            createUserCmd.UserRoles = new[] {
                new UserCommandRole { Role = roleOrgLevel, GrantedOrganisations = new[] { 1L, 2L } },
                new UserCommandRole { Role = roleAccLevel, GrantedAccounts = new[] { 3L, 4L } }
            };

            var user = User.Create(createUserCmd);
            Assert.Equal(createUserCmd.Name, user.Name);
            Assert.Equal(createUserCmd.Email, user.Email);
            Assert.Equal(createUserCmd.Login, user.Login);
            Assert.Equal(createUserCmd.Country, user.Country);
            Assert.Equal(createUserCmd.Currency, user.Currency);
            Assert.All(user.Roles, role
                => Assert.Contains(createUserCmd.UserRoles, cmdItem
                    => role.Role.Id == cmdItem.Role.Id
                    && ((role.Accounts == null && cmdItem.GrantedAccounts == null)
                       || role.Accounts?.SequenceEqual(cmdItem.GrantedAccounts ?? Enumerable.Empty<long>()) == true)
                    && ((role.Organisations == null && cmdItem.GrantedOrganisations == null)
                       || role.Organisations?.SequenceEqual(cmdItem.GrantedOrganisations ?? Enumerable.Empty<long>()) == true)));
        }



    }
}
