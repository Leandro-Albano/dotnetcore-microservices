using Dynasoft.Common.Api.Extensions;
using Dynasoft.Common.Domain.Entities.CommonQueryExtensions;
using Dynasoft.Security.Api;
using Dynasoft.Security.Api.Routes;
using Dynasoft.Security.Application.Contracts.ApplicationResource;
using Dynasoft.Security.Application.Contracts.DTOs;
using Dynasoft.Security.Infrastructure.Repositories;
using Dynasoft.Security.Tests.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using ServiceStack;

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

using Xunit;

namespace Dynasoft.Security.Tests.Integration.Api.Controllers
{
    public class ApplicationResourceControllerTests : IClassFixture<CustomWebApplicationFactory<Security.Api.Startup>>, IDisposable
    {
        private readonly CustomWebApplicationFactory<Startup> factory;
        private readonly HttpClient client;
        private readonly SecurityDataContext context;
        private readonly IServiceScope scope;

        public ApplicationResourceControllerTests(CustomWebApplicationFactory<Security.Api.Startup> factory)
        {
            this.factory = factory;
            this.client = factory.CreateClient();
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
            this.scope = this.factory.Services.CreateScope();

            var context = this.scope.ServiceProvider.GetRequiredService<SecurityDataContext>();
            this.context = context;
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            new SecurityDatabaseSeeder(context).Seed();
        }

        [Fact]
        public async Task CreateResource_ShouldReturnTheNewResourceId_WhenRequestIsValid()
        {
            var command = ApplicationResourceCmdGenerator.CreateApplicationResourceCommand;
            var response = await this.client
                .PostAsJsonAsync($"{ApplicationResourceApiRoutes.CONTROLLER}/{ApplicationResourceApiRoutes.CREATE_RESOURCE}", command);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var result = await response.Content.ReadAsStringAsync();
            int.TryParse(result, out int id);
            Assert.True(id > 0, $"Invalid id: {result}");
        }

        [Fact]
        public async Task GetResource_ShouldReturnCorrectResource_WhenIdExists()
        {
            var appResource = this.context.ApplicationResources.First();

            var getRoute = ApplicationResourceApiRoutes.GET_RESOURCE.Replace("{id}", appResource.Id.ToString());
            var getResponse = await this.client.GetAsync($"{ApplicationResourceApiRoutes.CONTROLLER}/{getRoute}");

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

            var found = await getResponse.Content.ReadFromJsonAsync<ApplicationResourceDto>(DefaultResponseJsonSerializerOptions.Options);
            Assert.Equal(appResource.Id, found.Id);
            Assert.Equal(appResource.Name, found.Name);
            Assert.Equal(appResource.ApplicationLevel, found.ApplicationLevel);
        }

        [Fact]
        public async Task SearchResource_ShouldReturnCorrectResources_WhenFilteringByIds()
        {
            var appResources = this.context.ApplicationResources
                .Include(c => c.AvailableActions)
                .ToArray();
            var ids = appResources.Select(r => r.Id).ToArray();
            var query = HttpUtility.ParseQueryString(string.Empty);
            var uriBuilder = new UriBuilder
            {
                Path = $"{ApplicationResourceApiRoutes.CONTROLLER}/{ApplicationResourceApiRoutes.SEARCH_RESOURCE}",
            };

            foreach (var id in ids)
                query.Add(nameof(SearchApplicationResourcesApplicationCommand.Ids), id.ToString());

            var getResponse = await this.client.GetAsync(uriBuilder.Uri);
            var found = await getResponse.Content.ReadFromJsonAsync<ApplicationResourceDto[]>(DefaultResponseJsonSerializerOptions.Options);

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.Equal(ids.Length, found.Length);
        }

        [Fact]
        public async Task UpdateResource_ShouldUpdateResource_WhenRequestIsValid()
        {
            var appResource = this.context.ApplicationResources.First();
            var command = ApplicationResourceCmdGenerator.UpdateApplicationResourceCommand;
            var uriBuilder = new UriBuilder
            {
                Path = $"{ApplicationResourceApiRoutes.CONTROLLER}/{ApplicationResourceApiRoutes.PATCH_RESOURCE.Replace("{applicationResourceId}", appResource.Id.ToString())}",
            };

            using (var content = new StringContent(JsonSerializer.Serialize(command), System.Text.Encoding.UTF8, MimeTypes.Json))
            {
                var patchResponse = await this.client.PatchAsync(uriBuilder.Uri, content);

                Assert.Equal(HttpStatusCode.NoContent, patchResponse.StatusCode);

                // Detach the used entities so we can query it fresh from database.
                this.context.Entry(appResource).State = EntityState.Detached;
                var resourceAfterRequest = await this.context.ApplicationResources.FindByIdAsync(appResource.Id);
                Assert.Equal(command.Name, resourceAfterRequest.Name);
                Assert.Equal(command.ApplicationLevel, resourceAfterRequest.ApplicationLevel);
            }

        }

        [Fact]
        public async Task UpdateResource_ShouldUpdateJustProvidedPropertiesOfResource_WhenOnlySomePropertyProvided()
        {
            var appResource = this.context.ApplicationResources.First();
            var command = ApplicationResourceCmdGenerator.UpdateApplicationResourceCommand;
            var uriBuilder = new UriBuilder
            {
                Path = $"{ApplicationResourceApiRoutes.CONTROLLER}/{ApplicationResourceApiRoutes.PATCH_RESOURCE.Replace("{applicationResourceId}", appResource.Id.ToString())}",
            };

            using (var content = new StringContent(JsonSerializer.Serialize(new { command.ApplicationLevel }), System.Text.Encoding.UTF8, MimeTypes.Json))
            {
                var patchResponse = await this.client.PatchAsync(uriBuilder.Uri, content);

                Assert.Equal(HttpStatusCode.NoContent, patchResponse.StatusCode);

                // Detach the used entities so we can query it fresh from database.
                this.context.Entry(appResource).State = EntityState.Detached;
                var resourceAfterRequest = await this.context.ApplicationResources.FindByIdAsync(appResource.Id);
                Assert.Equal(appResource.Name, resourceAfterRequest.Name);
                Assert.Equal(command.ApplicationLevel, resourceAfterRequest.ApplicationLevel);
            }

        }

        [Fact]
        public async Task DeleteResource_ShouldDeleteResource_WhenRequestIsValid()
        {
            var appResource = this.context.ApplicationResources.First();

            var uriBuilder = new UriBuilder
            {
                Path = $"{ApplicationResourceApiRoutes.CONTROLLER}/{ApplicationResourceApiRoutes.DELETE_RESOURCE.Replace("{applicationResourceId}", appResource.Id.ToString())}",
            };

            var patchResponse = await this.client.DeleteAsync(uriBuilder.Uri);

            Assert.Equal(HttpStatusCode.NoContent, patchResponse.StatusCode);

            // Detach the used entities so we can query it fresh from database.
            this.context.Entry(appResource).State = EntityState.Detached;
            var resourceAfterRequest = await this.context.ApplicationResources.FindByIdAsync(appResource.Id);
            Assert.Null(resourceAfterRequest);
        }

        [Fact]
        public async Task AddAction_ShouldAddActionToResource_WhenRequestIsValid()
        {
            var command = ApplicationResourceCmdGenerator.AddResourceActionCommand;
            var appResource = this.context.ApplicationResources.First();
            var id = appResource.Id;
            var uriBuilder = new UriBuilder
            {
                Path = $"{ApplicationResourceApiRoutes.CONTROLLER}/{ApplicationResourceApiRoutes.ADD_ACTION.Replace("{applicationResourceId}", id.ToString())}",
            };

            var response = await this.client.PostAsJsonAsync(uriBuilder.Uri, command, DefaultResponseJsonSerializerOptions.Options);
            var result = await response.Content.ReadAsStringAsync();
            int.TryParse(result, out int createdId);
            var created = await this.context.ApplicationResources
                .Include(a => a.AvailableActions)
                .FindByIdAsync(id);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(created.AvailableActions.SingleOrDefault(a => a.Id == createdId));

        }

        [Fact]
        public async Task RemoveAction_ShouldRemoveActionFromResource_WhenRequestIsValid()
        {
            var appResource = this.context.ApplicationResources.Include(a => a.AvailableActions).First();
            var id = appResource.Id;
            var actionToRemove = appResource.AvailableActions.First();
            var uriBuilder = new UriBuilder
            {
                Path = @$"{ApplicationResourceApiRoutes.CONTROLLER}/{ApplicationResourceApiRoutes.REMOVE_ACTION.Replace("{applicationResourceId}", id.ToString())
                                                                                                                 .Replace("{actionId}", actionToRemove.Id.ToString())}",
            };

            var response = await this.client.DeleteAsync(uriBuilder.Uri);

            // Detach the used entities so we can query it fresh from database.
            this.context.Entry(appResource).State = EntityState.Detached;
            this.context.Entry(actionToRemove).State = EntityState.Detached;

            var resourceAfterRequest = await this.context.ApplicationResources
                .Include(a => a.AvailableActions)
                .FindByIdAsync(id);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Null(resourceAfterRequest.AvailableActions.SingleOrDefault(a => a.Id == actionToRemove.Id));
        }

        [Fact]
        public async Task PatchAction_ShouldUpdateActionDefinition_WhenRequestIsValid()
        {
            var command = ApplicationResourceCmdGenerator.UpdateResourceActionApplicationCommand;
            var appResource = this.context.ApplicationResources.Include(a => a.AvailableActions).First();
            var id = appResource.Id;
            var actionToPatch = appResource.AvailableActions.First();
            var uriBuilder = new UriBuilder
            {
                Path = @$"{ApplicationResourceApiRoutes.CONTROLLER}/{ApplicationResourceApiRoutes.PATCH_ACTION.Replace("{applicationResourceId}", id.ToString())
                                                                                                                 .Replace("{actionId}", actionToPatch.Id.ToString())}",
            };

            using (var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(command), System.Text.Encoding.UTF8, MimeTypes.Json))
            {
                var response = await this.client.PatchAsync(uriBuilder.Uri, content);

                // Detach the used entities so we can query it fresh from database.
                this.context.Entry(appResource).State = EntityState.Detached;
                this.context.Entry(actionToPatch).State = EntityState.Detached;
                var resourceAfterRequest = await this.context.ApplicationResources
                    .Include(a => a.AvailableActions)
                    .FindByIdAsync(id);
                var patchedAction = resourceAfterRequest.AvailableActions.FindById(actionToPatch.Id);

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
                Assert.Equal(command.Name, patchedAction.Name);
            }
        }

        public void Dispose() => this.scope.Dispose();

    }
}
