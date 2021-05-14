using Dynasoft.Common.Api.Controllers;
using Dynasoft.Common.Domain.Contracts.Exceptions;
using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Api.Routes;
using Dynasoft.Security.Application.Contracts.DTOs;
using Dynasoft.Security.Application.Contracts.Role;
using Dynasoft.Security.Domain.Entities.ResourceAggregate;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dynasoft.Security.Api.Controllers
{
    [Route(RoleApiRoutes.CONTROLLER)]
    [ApiController]
    public class RolesController : AbstractApiController
    {
        private readonly IMediator mediator;

        public RolesController(IMediator mediator) => this.mediator = mediator;

        [HttpPost(RoleApiRoutes.CREATE_ROLE)]
        public async Task<ActionResult<int>> CreateRole(CreateRoleApplicationCommand command)
        {
            var response = await this.mediator.Send<CreateRoleApplicationCommand, RoleDto>(command);

            return this.Created(response.Id);
        }

        [HttpGet(RoleApiRoutes.GET_ROLE)]
        public async Task<ActionResult<RoleDto>> GetRole(long id)
        {
            var response = (await this.mediator
                .Send<SearchRolesApplicationCommand, IEnumerable<RoleDto>>(new SearchRolesApplicationCommand
                {
                    Ids = new[] { id }
                })).SingleOrDefault() ?? throw new EntityNotFoundException(nameof(ApplicationResource), id);

            return response;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> SearchRole([FromQuery] SearchRolesApplicationCommand search)
        {
            var response = await this.mediator
                .Send<SearchRolesApplicationCommand, IEnumerable<RoleDto>>(search);

            return this.Ok(response);
        }

        [HttpPatch(RoleApiRoutes.PATCH_ROLE)]
        public async Task<IActionResult> UpdateRole([FromRoute] long roleId, [FromBody] UpdateRoleApplicationCommand command)
        {
            command.Id = roleId;
            await this.mediator.Send(command);

            return this.NoContent();
        }

        [HttpDelete(RoleApiRoutes.DELETE_ROLE)]
        public async Task<IActionResult> DeleteRole([FromRoute] long roleId)
        {
            await this.mediator.Send(new DeleteRoleApplicationCommand { Id = roleId });

            return this.NoContent();
        }

        [HttpPut(RoleApiRoutes.UPDATE_PERMISSIONS)]
        public async Task<IActionResult> UpdateRolePermissions([FromRoute] long roleId, UpdateRolePermissionApplicationCommand command)
        {
            command.RoleId = roleId;
            await this.mediator.Send(command);
            return this.NoContent();
        }
    }
}
