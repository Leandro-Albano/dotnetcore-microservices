
using Dynasoft.Common.Domain.Contracts.Exceptions;
using Dynasoft.Security.Domain.Entities;
using Dynasoft.Security.Domain.Entities.ResourceAggregate;
using Dynasoft.Security.Domain.Entities.ResourceAggregate.Commands;
using Dynasoft.Security.Domain.Events;
using Dynasoft.Security.Domain.Events.ApplicationResource;
using Dynasoft.Security.Tests.Common;

using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Dynasoft.Security.Tests.Unit.Domain.Entities.ResourceAggregate
{
    public class ApplicationResourceTests
    {
        public static IEnumerable<object[]> AddResourceActionCmdTestItems = new[]{
            new object[]{ new AddResourceActionCommand { }, new[] {
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(AddResourceActionCommand.Name) }
            } },
            new object[]{ new AddResourceActionCommand { Name = "" }, new[] {
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(AddResourceActionCommand.Name) }
            } },
            new object[]{ new AddResourceActionCommand { Name = " " }, new[] {
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(AddResourceActionCommand.Name) }
            } }
        };

        public static IEnumerable<object[]> UpdateResourceActionCmdTestItems = new[]{
            new object[]{ new UpdateResourceActionCommand { }, new[] {
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(UpdateResourceActionCommand.Name) },
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(UpdateResourceActionCommand.Action) }
            } },
            new object[]{ new UpdateResourceActionCommand { Name = "" }, new[] {
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(UpdateResourceActionCommand.Name) },
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(UpdateResourceActionCommand.Action) }
            } },
            new object[]{ new UpdateResourceActionCommand { Name = "    " }, new[] {
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(UpdateResourceActionCommand.Name) },
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(UpdateResourceActionCommand.Action) }
            } }
        };

        public static IEnumerable<object[]> CreateAppResourceCmdTestItems = new[] {
            new object[] {new CreateApplicationResourceCommand { ApplicationLevel = Security.Domain.Contracts.Common.ApplicationLevel.Account }, new []{
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(CreateApplicationResourceCommand.Name) }
            } },
            new object[] {new CreateApplicationResourceCommand { Name = "", ApplicationLevel = Security.Domain.Contracts.Common.ApplicationLevel.Application }, new []{
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(CreateApplicationResourceCommand.Name) }
            } },
            new object[] {new CreateApplicationResourceCommand { Name = "   ", ApplicationLevel = Security.Domain.Contracts.Common.ApplicationLevel.Organisation}, new []{
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(CreateApplicationResourceCommand.Name) }
            } },
            new object[] {new CreateApplicationResourceCommand { Name = "Valid" }, new []{
                new ValidationError { Code = ValidationErrorCode.InvalidValue, Member = nameof(CreateApplicationResourceCommand.ApplicationLevel) }
            } },
            new object[] {new CreateApplicationResourceCommand { Name = "Valid", ApplicationLevel = Security.Domain.Contracts.Common.ApplicationLevel.NotSet }, new []{
                new ValidationError { Code = ValidationErrorCode.InvalidValue, Member = nameof(CreateApplicationResourceCommand.ApplicationLevel) }
            } }
        };

        public static IEnumerable<object[]> UpdateAppResourceCmdTestItems = new[] {
            new object[] {new UpdateApplicationResourceCommand { ApplicationLevel = Security.Domain.Contracts.Common.ApplicationLevel.Account }, new []{
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(CreateApplicationResourceCommand.Name) }
            } },
            new object[] {new UpdateApplicationResourceCommand { Name = "", ApplicationLevel = Security.Domain.Contracts.Common.ApplicationLevel.Application }, new []{
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(CreateApplicationResourceCommand.Name) }
            } },
            new object[] {new UpdateApplicationResourceCommand { Name = "   ", ApplicationLevel = Security.Domain.Contracts.Common.ApplicationLevel.Organisation}, new []{
                new ValidationError { Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(CreateApplicationResourceCommand.Name) }
            } },
            new object[] {new UpdateApplicationResourceCommand { Name = "Valid" }, new []{
                new ValidationError { Code = ValidationErrorCode.InvalidValue, Member = nameof(CreateApplicationResourceCommand.ApplicationLevel) }
            } },
            new object[] {new UpdateApplicationResourceCommand { Name = "Valid", ApplicationLevel = Security.Domain.Contracts.Common.ApplicationLevel.NotSet }, new []{
                new ValidationError { Code = ValidationErrorCode.InvalidValue, Member = nameof(CreateApplicationResourceCommand.ApplicationLevel) }
            } }
        };

        public static IEnumerable<object[]> RemoveResourceActionCmdTestItems = new[] {
            new object[]{ new RemoveResourceActionCommand { }, new[] {
                new ValidationError{ Code = ValidationErrorCode.MissingRequiredMember, Member = nameof(RemoveResourceActionCommand.Action)}
            } }
        };

        [Theory]
        [MemberData(nameof(CreateAppResourceCmdTestItems))]
        public void Create_ShoudThrowDomainValidationException_WhenCommandIsInvalid(CreateApplicationResourceCommand command, ValidationError[] errors)
        {
            var ex = Assert.Throws<DomainValidationException>(() => ApplicationResource.Create(command));

            Assert.Equal(errors.Length, ex.ValidationErrors.Count());
            Assert.All(ex.ValidationErrors, e => errors.Any(er => er.Code == e.Code && er.Member == e.Member));
        }

        [Fact]
        public void Create_ShoudAddApplicationResourceCreatedEvent_WhenCommandIsValid()
        {
            var command = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var newResource = ApplicationResource.Create(command);

            Assert.Single(newResource.Events);
            Assert.IsType<ApplicationResourceCreatedEvent>(newResource.Events.Single());
        }

        [Theory]
        [MemberData(nameof(UpdateAppResourceCmdTestItems))]
        public void Update_ShoudThrowDomainValidationException_WhenCommandIsInvalid(UpdateApplicationResourceCommand command, ValidationError[] errors)
        {
            var createCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var newResource = ApplicationResource.Create(createCmd);

            var ex = Assert.Throws<DomainValidationException>(() => newResource.Update(command));

            Assert.Equal(errors.Length, ex.ValidationErrors.Count());
            Assert.All(ex.ValidationErrors, e => errors.Any(er => er.Code == e.Code && er.Member == e.Member));
        }

        [Fact]
        public void Update_ShoudUpdatePropertiesCorrectly_WhenCommandIsValid()
        {
            var createCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var newResource = ApplicationResource.Create(createCmd);
            var updateCommand = ApplicationResourceCmdGenerator.UpdateApplicationResourceCommand;

            newResource.Update(updateCommand);

            Assert.Equal(updateCommand.Name, newResource.Name);
            Assert.Equal(updateCommand.ApplicationLevel, newResource.ApplicationLevel);
        }

        [Fact]
        public void Update_ShoudAddApplicationResourceUpdatedEvent_WhenCommandIsValid()
        {
            var createCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var newResource = ApplicationResource.Create(createCmd);
            var updateCommand = ApplicationResourceCmdGenerator.UpdateApplicationResourceCommand;

            newResource.Update(updateCommand);
            var @event = Assert.IsType<ApplicationResourceUpdatedEvent>(newResource.Events.Last());


            Assert.Equal(updateCommand.Name, @event.Name);
            Assert.Equal(updateCommand.ApplicationLevel, @event.ApplicationLevel);
        }

        [Fact]
        public void Delete_ShoudAddApplicationResourceDeletedEvent_WhenCalled()
        {
            var createCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var newResource = ApplicationResource.Create(createCmd);

            var newResourceIdProp = newResource.GetType().GetProperty(nameof(ApplicationResource.Id));
            newResourceIdProp.SetValue(newResource, 42);

            newResource.Delete();
            var @event = Assert.IsType<ApplicationResourceDeletedEvent>(newResource.Events.Last());

            Assert.Equal(newResource.Id, @event.Id);
        }

        [Theory]
        [MemberData(nameof(AddResourceActionCmdTestItems))]
        public void AddResourceAction_ShouThrowDomainValidationException_WhenCommandIsInvalid(AddResourceActionCommand command, ValidationError[] errors)
        {
            var createCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var newResource = ApplicationResource.Create(createCmd);

            var ex = Assert.Throws<DomainValidationException>(() => newResource.AddResourceAction(command));

            Assert.Equal(errors.Length, ex.ValidationErrors.Count());
            Assert.All(ex.ValidationErrors, e => errors.Any(er => er.Code == e.Code && er.Member == e.Member));
        }

        [Fact]
        public void AddResourceAction_ShouAddTheNewActionToAvailableActionsList_WhenCommandIsValid()
        {
            var createCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var newResource = ApplicationResource.Create(createCmd);
            var addActionCommand = ApplicationResourceCmdGenerator.AddResourceActionCommand;
            newResource.AddResourceAction(addActionCommand);

            var action = newResource.AvailableActions.Single();
            Assert.Equal(addActionCommand.Name, action.Name);
        }

        [Fact]
        public void AddResourceAction_ShouReturnTheNewAddedAction_WhenCommandIsValid()
        {
            var createCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var newResource = ApplicationResource.Create(createCmd);
            var addActionCommand = ApplicationResourceCmdGenerator.AddResourceActionCommand;
            var action = newResource.AddResourceAction(addActionCommand);

            Assert.Equal(addActionCommand.Name, action.Name);
        }

        [Fact]
        public void AddResourceAction_ShouAddNewResourceActionAddedEvent_WhenNewResourceIsAdded()
        {
            var createCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var newResource = ApplicationResource.Create(createCmd);

            var addActionCommand = ApplicationResourceCmdGenerator.AddResourceActionCommand;
            var action = newResource.AddResourceAction(addActionCommand);

            Assert.Equal(2, newResource.Events.Count());

            var addedEvent = Assert.IsType<NewResourceActionAddedEvent>(newResource.Events.Last());

            Assert.Equal(action.Name, addedEvent.Name);
        }

        [Theory]
        [MemberData(nameof(RemoveResourceActionCmdTestItems))]
        public void RemoveResourceAction_ShouldThrowDomainValidationException_WhenCommandIsInvalid(RemoveResourceActionCommand command,
            ValidationError[] errors)
        {
            var newResource = ApplicationResource.Create(ApplicationResourceCmdGenerator.CreateApplicationResourceCommand);

            var ex = Assert.Throws<DomainValidationException>(() => newResource.RemoveResourceAction(command));

            Assert.All(ex.ValidationErrors, e => Assert.Contains(e, errors));
        }

        [Fact]
        public void RemoveResourceAction_ShouldThrowDomainValidationException_WhenActionDoesNotBelongToResource()
        {
            var newResource = ApplicationResource.Create(ApplicationResourceCmdGenerator.CreateApplicationResourceCommand);
            var newResourceAction = newResource.AddResourceAction(ApplicationResourceCmdGenerator.AddResourceActionCommand);

            var otherResource = ApplicationResource.Create(ApplicationResourceCmdGenerator.CreateApplicationResourceCommand);
            var otherResourceAction = otherResource.AddResourceAction(ApplicationResourceCmdGenerator.AddResourceActionCommand);

            var ex = Assert.Throws<DomainValidationException>(() => newResource.RemoveResourceAction(new RemoveResourceActionCommand
            {
                Action = otherResourceAction
            }));

            Assert.Single(ex.ValidationErrors);
            Assert.Equal(ex.ValidationErrors.Single(),
                         new ValidationError { Code = ValidationErrorCode.InvalidReferece, Member = nameof(RemoveResourceActionCommand.Action) });
        }

        [Fact]
        public void RemoveResourceAction_ShouldRemoveOnlyTheRightItemFromCollection_WhenCommandIsValid()
        {
            var createCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var newResource = ApplicationResource.Create(createCmd);

            var addActionCommand = ApplicationResourceCmdGenerator.AddResourceActionCommand;
            var action = newResource.AddResourceAction(addActionCommand);
            var action2 = newResource.AddResourceAction(ApplicationResourceCmdGenerator.AddResourceActionCommand);

            newResource.RemoveResourceAction(new RemoveResourceActionCommand { Action = action });

            Assert.DoesNotContain(action, newResource.AvailableActions);
            Assert.Contains(action2, newResource.AvailableActions);
        }

        [Fact]
        public void RemoveResourceAction_ShouldAddApplicationResourceActionRemovedEvent_WhenCommandIsValid()
        {
            var createCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var newResource = ApplicationResource.Create(createCmd);

            var addActionCommand = ApplicationResourceCmdGenerator.AddResourceActionCommand;
            var action = newResource.AddResourceAction(addActionCommand);

            // Set the Id through reflaction because it's private and generated by the database.
            // Doing this we can confirm that the event contains the right informationl
            var actionIdProp = action.GetType().GetProperty(nameof(ResourceAction.Id));
            actionIdProp.SetValue(action, 42);

            var newResourceIdProp = newResource.GetType().GetProperty(nameof(ApplicationResource.Id));
            newResourceIdProp.SetValue(newResource, 42);

            newResource.RemoveResourceAction(new RemoveResourceActionCommand { Action = action });
            var @event = Assert.IsType<ApplicationResourceActionRemovedEvent>(newResource.Events.Last());
            Assert.Equal(newResource.Id, @event.Id);
            Assert.Equal(action.Id, @event.ResourceActionId);
        }

        [Theory]
        [MemberData(nameof(UpdateResourceActionCmdTestItems))]
        public void UpdateResourceAction_ShouldThrowDomainValidationException_WhenCommandIsInvalid(UpdateResourceActionCommand command, ValidationError[] errors)
        {
            var createCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var resource = ApplicationResource.Create(createCmd);

            var ex = Assert.Throws<DomainValidationException>(() => resource.UpdateAction(command));

            Assert.Equal(errors.Length, ex.ValidationErrors.Count());
            Assert.All(ex.ValidationErrors, err => errors.Contains(err));
        }

        [Fact]
        public void UpdateResourceAction_ShouldThrowDomainValidationException_WhenActionDoesNotBelongToResource()
        {
            var createCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var resource = ApplicationResource.Create(createCmd);
            var resource2 = ApplicationResource.Create(createCmd);
            var addActionCmd = ApplicationResourceCmdGenerator.AddResourceActionCommand;

            // Add action to resource 2 and try to update on resource 1
            var action = resource2.AddResourceAction(addActionCmd);
            var updateActionCmd = ApplicationResourceCmdGenerator.UpdateResourceActionCommand;
            updateActionCmd.Action = action;

            var ex = Assert.Throws<DomainValidationException>(() => resource.UpdateAction(updateActionCmd));

            Assert.Single(ex.ValidationErrors);
            Assert.Equal(ValidationErrorCode.InvalidReferece, ex.ValidationErrors.Single().Code);
            Assert.Equal(nameof(UpdateResourceActionCommand.Action), ex.ValidationErrors.Single().Member);
        }

        [Fact]
        public void UpdateResourceAction_ShouldUpdateTheRightAction_WhenCommandIsValid()
        {
            var createCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var resource = ApplicationResource.Create(createCmd);
            var action = resource.AddResourceAction(ApplicationResourceCmdGenerator.AddResourceActionCommand);
            var action2 = resource.AddResourceAction(ApplicationResourceCmdGenerator.AddResourceActionCommand);

            var updateActionCmd = ApplicationResourceCmdGenerator.UpdateResourceActionCommand;
            updateActionCmd.Action = action;

            resource.UpdateAction(updateActionCmd);

            Assert.Equal(updateActionCmd.Name, action.Name);
            Assert.NotEqual(updateActionCmd.Name, action2.Name);
        }

        [Fact]
        public void UpdateResourceAction_ShouldAddApplicationResourceActionUpdatedEvent_WhenCommandIsValid()
        {
            var createCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var resource = ApplicationResource.Create(createCmd);
            var action = resource.AddResourceAction(ApplicationResourceCmdGenerator.AddResourceActionCommand);

            var updateActionCmd = ApplicationResourceCmdGenerator.UpdateResourceActionCommand;
            updateActionCmd.Action = action;

            resource.UpdateAction(updateActionCmd);

            var @event = Assert.IsType<ApplicationResourceActionUpdatedEvent>(resource.Events.Last());
            Assert.Equal(resource.Id, @event.Id);
            Assert.Equal(action.Id, @event.ResourceActionId);
            Assert.Equal(action.Name, @event.Name);
        }

        [Fact]
        public void ToString_ShouldReturnCorrectFormat()
        {
            var createCmd = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var resource = ApplicationResource.Create(createCmd);

            Assert.Equal($"{nameof(ApplicationResource)}: {resource.Id}-{resource.Name}", resource.ToString());
        }

    }
}
