using IndeedIQ.Common.Domain.Entities;
using IndeedIQ.Security.Domain.Contracts.Common;
using IndeedIQ.Security.Domain.Entities.ResourceAggregate.Commands;
using IndeedIQ.Security.Domain.Events;
using IndeedIQ.Security.Domain.Events.ApplicationResource;

using System.Collections.Generic;

namespace IndeedIQ.Security.Domain.Entities.ResourceAggregate
{
    public class ApplicationResource : AggregateRoot<ApplicationResource>, IDeletableEntity
    {
        public string Name { get; private set; }
        public ApplicationLevel ApplicationLevel { get; private set; }

        private readonly List<ResourceAction> availableActions = new List<ResourceAction>();
        public IReadOnlyCollection<ResourceAction> AvailableActions => this.availableActions.AsReadOnly();

        public static ApplicationResource Create(CreateApplicationResourceCommand command)
        {

            command.Validate(true);
            var created = new ApplicationResource
            {
                Name = command.Name,
                ApplicationLevel = command.ApplicationLevel
            };

            created.AddEvent(new ApplicationResourceCreatedEvent(created));
            return created;
        }

        public ResourceAction AddResourceAction(AddResourceActionCommand command)
        {
            command.Validate(true);

            var action = new ResourceAction
            {
                Name = command.Name,
                Resource = this
            };

            this.availableActions.Add(action);
            this.AddEvent(new NewResourceActionAddedEvent(this) { Name = action.Name });

            return action;
        }

        public void RemoveResourceAction(RemoveResourceActionCommand command)
        {
            command.Validate(this, true);
            command.Action.Resource = null;
            //command.Action.ApplicationResourceId = default;
            this.availableActions.Remove(command.Action);
            this.AddEvent(new ApplicationResourceActionRemovedEvent(this)
            {
                ResourceActionId = command.Action.Id
            });
        }

        public void UpdateAction(UpdateResourceActionCommand command)
        {
            command.Validate(this, true);
            command.Action.Name = command.Name;
            this.AddEvent(new ApplicationResourceActionUpdatedEvent(this)
            {
                ResourceActionId = command.Action.Id,
                Name = command.Action.Name
            });
        }

        public void Update(UpdateApplicationResourceCommand command)
        {
            command.Validate(true);
            this.Name = command.Name;
            this.ApplicationLevel = command.ApplicationLevel;

            this.AddEvent(new ApplicationResourceUpdatedEvent(this)
            {
                ApplicationLevel = this.ApplicationLevel,
                Name = this.Name
            });
        }

        public void Delete()
        {
            this.AddEvent(new ApplicationResourceDeletedEvent(this));
        }

        #region Overrides
        public override string ToString() => $"{nameof(ApplicationResource)}: {this.Id}-{this.Name}";
        #endregion

    }
}
