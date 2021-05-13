using IndeedIQ.Common.Domain.Entities;
using IndeedIQ.Security.Domain.Entities.RoleAggregate.Commands;
using IndeedIQ.Security.Domain.Events;
using IndeedIQ.Security.Domain.Events.Role;
using IndeedIQ.Security.Tests.Unit.Domain.Entities.RoleAggregate;

using System.Collections.Generic;
using System.Linq;

namespace IndeedIQ.Security.Domain.Entities.RoleAggregate
{
    public class Role : AggregateRoot<Role>, IDeletableEntity
    {
        public string Name { get; set; }

        private readonly List<RoleActionPermission> rolePermissions = new List<RoleActionPermission>();
        public IReadOnlyCollection<RoleActionPermission> RolePermissions => this.rolePermissions.AsReadOnly();

        public static Role Create(CreateRoleCommand command)
        {
            command.Validate(true);
            var role = new Role
            {
                Name = command.Name,
            };

            role.AddEvent(new RoleCreatedEvent(role) { Name = role.Name });

            return role;
        }

        public void Update(UpdateRoleCommand command)
        {
            command.Validate(true);

            this.Name = command.Name;

            this.AddEvent(new RoleUpdatedEvent(this) { Name = this.Name });
        }

        public void UpdatePermissions(UpdateRolePermissionsCommand command)
        {
            command.Validate(this, true);

            this.rolePermissions.Clear();
            this.rolePermissions.AddRange(command.Permissions.Select(p => new RoleActionPermission
            {
                Action = p.Action,
                Permission = p.Permission
            }));

            this.AddEvent(new RolePermissionsUpdatedEvent(this)
            {
                Permissions = this.rolePermissions.Select(p => new Contracts.Role.RoleActionPermissionDto
                {
                    Action = new Contracts.ApplicationResource.ResourceActionDto
                    {
                        Id = p.Action.Id,
                        Name = p.Action.Name
                    },
                    Permission = p.Permission
                })
            });
        }

        public void Delete() => this.AddEvent(new RoleDeletedEvent(this));

        public override string ToString()
            => $"{nameof(Role)}: {this.Id}-{this.Name}";

    }
}
