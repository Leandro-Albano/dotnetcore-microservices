using Dynasoft.Common.Api.Controllers;
using Dynasoft.Common.Domain.Contracts.Exceptions;
using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Api.Routes;
using Dynasoft.Security.Application.Contracts.DTOs;
using Dynasoft.Security.Application.Contracts.Role;
using Dynasoft.Security.Application.Contracts.User;
using Dynasoft.Security.Domain.Entities.ResourceAggregate;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dynasoft.Security.Api.Controllers
{
    [Route(UsersApiRoutes.CONTROLLER)]
    [ApiController]
    public class UsersController : AbstractApiController
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator) => this.mediator = mediator;

        [HttpPost(UsersApiRoutes.CREATE_USER)]
        public async Task<ActionResult<int>> CreateRole(CreateUserApplicationCommand command)
        {
            var response = await this.mediator.Send<CreateUserApplicationCommand, UserDto>(command);

            return this.Created(response.IdentityServerId);
        }

        [HttpGet(UsersApiRoutes.GET_USER)]
        public async Task<ActionResult<UserDto>> GetUser(long id)
        {
            var response = (await this.mediator
                .Send<SearchUserApplicationCommand, IEnumerable<UserDto>>(new SearchUserApplicationCommand
                {
                    Ids = new[] { id }
                })).SingleOrDefault() ?? throw new EntityNotFoundException(nameof(ApplicationResource), id);

            return response;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> SearchUser([FromQuery] SearchUserApplicationCommand search)
        {
            var response = await this.mediator
                .Send<SearchUserApplicationCommand, IEnumerable<UserDto>>(search);

            return this.Ok(response);
        }

        [HttpPatch(UsersApiRoutes.PATCH_USER)]
        public async Task<IActionResult> UpdateUser([FromRoute] long roleId, [FromBody] UpdateRoleApplicationCommand command)
        {
            command.Id = roleId;
            await this.mediator.Send(command);

            return this.NoContent();
        }

        [HttpDelete(UsersApiRoutes.DELETE_USER)]
        public async Task<IActionResult> DeleteUser([FromRoute] long roleId)
        {
            await this.mediator.Send(new DeleteRoleApplicationCommand { Id = roleId });

            return this.NoContent();
        }
    }
}
