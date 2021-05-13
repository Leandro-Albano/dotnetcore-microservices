using IndeedIQ.Common.Api.Controllers;
using IndeedIQ.Common.Domain.Contracts.Exceptions;
using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Security.Application.Contracts.ApplicationResource;
using IndeedIQ.Security.Application.Contracts.DTOs;
using IndeedIQ.Security.Domain.Entities.ResourceAggregate;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndeedIQ.Security.Api.Controllers
{
    [Route(Routes.ApplicationResourceApiRoutes.CONTROLLER)]
    [ApiController]
    public class ApplicationResourcesController : AbstractApiController
    {
        private readonly IMediator mediator;

        public ApplicationResourcesController(IMediator mediator) => this.mediator = mediator;

        [HttpPost(Routes.ApplicationResourceApiRoutes.CREATE_RESOURCE)]
        public async Task<ActionResult<int>> CreateResource(CreateApplicationResourceApplicationCommand command)
        {
            var response = await this.mediator.Send<CreateApplicationResourceApplicationCommand, ApplicationResourceDto>(command);

            return this.Created(response.Id);
        }

        [HttpGet(Routes.ApplicationResourceApiRoutes.GET_RESOURCE)]
        public async Task<ActionResult<ApplicationResourceDto>> GetResource(long id)
        {
            var response = (await this.mediator
                .Send<SearchApplicationResourcesApplicationCommand, IEnumerable<ApplicationResourceDto>>(new SearchApplicationResourcesApplicationCommand
                {
                    Ids = new[] { id }
                })).SingleOrDefault() ?? throw new EntityNotFoundException(nameof(ApplicationResource), id);

            return response;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationResourceDto>>> SearchResource(
            [FromQuery] SearchApplicationResourcesApplicationCommand search)
        {
            var response = await this.mediator
                .Send<SearchApplicationResourcesApplicationCommand, IEnumerable<ApplicationResourceDto>>(search);

            return this.Ok(response);
        }

        [HttpPatch(Routes.ApplicationResourceApiRoutes.PATCH_RESOURCE)]
        public async Task<IActionResult> UpdateResource([FromRoute] long applicationResourceId,
            [FromBody] UpdateApplicationResourceApplicationCommand command)
        {
            command.Id = applicationResourceId;
            await this.mediator.Send(command);

            return this.NoContent();
        }

        [HttpDelete(Routes.ApplicationResourceApiRoutes.DELETE_RESOURCE)]
        public async Task<IActionResult> DeleteResource([FromRoute] long applicationResourceId)
        {
            await this.mediator.Send(new DeleteApplicationResourceCommand { Id = applicationResourceId });

            return this.NoContent();
        }

        [HttpPost(Routes.ApplicationResourceApiRoutes.ADD_ACTION)]
        public async Task<IActionResult> AddAction([FromRoute] long applicationResourceId,
                                                              AddResourceActionApplicationCommand command)
        {
            command.ApplicationResourceId = applicationResourceId;
            var id = await this.mediator
                .Send<AddResourceActionApplicationCommand, long>(command);

            return this.Created(id);
        }

        [HttpDelete(Routes.ApplicationResourceApiRoutes.REMOVE_ACTION)]
        public async Task<IActionResult> RemoveAction([FromRoute] long applicationResourceId, [FromRoute] long actionId)
        {
            await this.mediator.Send(new RemoveResourceActionApplicationCommand
            {
                ApplicationResourceId = applicationResourceId,
                ResourceActionId = actionId
            });

            return this.NoContent();
        }

        [HttpPatch(Routes.ApplicationResourceApiRoutes.PATCH_ACTION)]
        public async Task<IActionResult> UpdateAction(
            [FromRoute] long applicationResourceId, [FromRoute] long actionId, [FromBody] UpdateResourceActionApplicationCommand command)
        {
            await this.mediator.Send(new UpdateResourceActionApplicationCommand
            {
                ApplicationResourceId = applicationResourceId,
                ResourceActionId = actionId,
                Name = command?.Name
            });

            return this.NoContent();
        }


    }
}
