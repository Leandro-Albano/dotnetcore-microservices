using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Security.Application.Contracts.Role;
using IndeedIQ.Security.Domain.Entities;
using IndeedIQ.Security.Domain.Entities.RoleAggregate.Commands;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace IndeedIQ.Security.Api.Handlers.Role
{
    public class UpdateRolePermissionApplicationCommandHandler : IMessageHandler<UpdateRolePermissionApplicationCommand>
    {
        public UpdateRolePermissionApplicationCommandHandler(ISecurityDataContext context) => this.Context = context;

        public ISecurityDataContext Context { get; }

        public async Task HandleAsync(UpdateRolePermissionApplicationCommand message)
        {
            var role = await this.Context.Roles
                .Include(r => r.RolePermissions)
                    .ThenInclude(c => c.Action)
                        .ThenInclude(a => a.Resource)
                .FindByIdAsync(message.RoleId);

            var actions = await this.Context.ApplicationResources
                .SelectMany(r => r.AvailableActions)
                .Where(a => message.Permissions.Select(p => p.ActionId).Contains(a.Id))
                .ToDictionaryAsync(r => r.Id, r => r);

            var updateCmd = new UpdateRolePermissionsCommand
            {
                Permissions = message.Permissions.Select(r => new UpdateRolePermissionItem { Action = actions[r.ActionId], Permission = r.Permission })
            };

            role.UpdatePermissions(updateCmd);
            await this.Context.PersistChangesAsync();
        }
    }
}
