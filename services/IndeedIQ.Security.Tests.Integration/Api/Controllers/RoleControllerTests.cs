using IndeedIQ.Common.Api.Extensions;
using IndeedIQ.Security.Api;
using IndeedIQ.Security.Api.Routes;
using IndeedIQ.Security.Application.Contracts.DTOs;
using IndeedIQ.Security.Application.Contracts.Role;
using IndeedIQ.Security.Infrastructure.Repositories;
using IndeedIQ.Security.Tests.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using ServiceStack;

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

using Xunit;

namespace IndeedIQ.Security.Tests.Integration.Api.Controllers
{
    public class RoleControllerTests : IClassFixture<CustomWebApplicationFactory<Security.Api.Startup>>, IDisposable
    {
        private readonly CustomWebApplicationFactory<Startup> factory;
        private readonly HttpClient client;
        private readonly SecurityDataContext context;
        private readonly IServiceScope scope;

        public RoleControllerTests(CustomWebApplicationFactory<Security.Api.Startup> factory)
        {
            this.factory = factory;
            this.client = factory.CreateClient();

            this.scope = this.factory.Services.CreateScope();
            var context = this.scope.ServiceProvider.GetRequiredService<SecurityDataContext>();

            this.context = context;
            this.context.Database.EnsureDeleted();
            this.context.Database.EnsureCreated();

            new SecurityDatabaseSeeder(context).Seed();
        }

        [Fact]
        public async Task CreateRole_ShouldReturnTheNewResourceId_WhenRequestIsValid()
        {
            var roleCmdGen = new RoleCmdGenerator();
            var command = roleCmdGen.CreateRoleCommand;
            var response = await this.client
                .PostAsJsonAsync($"{RoleApiRoutes.CONTROLLER}/{RoleApiRoutes.CREATE_ROLE}", command);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var result = await response.Content.ReadAsStringAsync();
            int.TryParse(result, out int id);
            Assert.True(id > 0, $"Invalid id: {result}");
        }

        [Fact]
        public async Task GetRole_ShouldReturnCorrectResource_WhenIdExists()
        {
            var role = this.context.Roles.First();

            var getRoute = RoleApiRoutes.GET_ROLE.Replace("{id}", role.Id.ToString());
            var getResponse = await this.client.GetAsync($"{RoleApiRoutes.CONTROLLER}/{getRoute}");

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

            var found = await getResponse.Content.ReadFromJsonAsync<RoleDto>(DefaultResponseJsonSerializerOptions.Options);
            Assert.Equal(role.Id, found.Id);
            Assert.Equal(role.Name, found.Name);
        }

        [Fact]
        public async Task SearchRole_ShouldReturnCorrectRoles_WhenFilteringByIds()
        {
            var roles = this.context.Roles
                .Include(c => c.RolePermissions)
                .ToArray();
            var ids = roles.Select(r => r.Id).ToArray();
            var query = HttpUtility.ParseQueryString(string.Empty);
            var uriBuilder = new UriBuilder
            {
                Path = $"{RoleApiRoutes.CONTROLLER}/{RoleApiRoutes.SEARCH_ROLE}",
            };

            foreach (var id in ids)
                query.Add(nameof(SearchRolesApplicationCommand.Ids), id.ToString());

            var getResponse = await this.client.GetAsync(uriBuilder.Uri);
            var found = await getResponse.Content.ReadFromJsonAsync<RoleDto[]>(DefaultResponseJsonSerializerOptions.Options);

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.Equal(ids.Length, found.Length);
        }

        [Fact]
        public async Task UpdateRole_ShouldUpdateRole_WhenRequestIsValid()
        {
            var role = this.context.Roles.First();
            var roleCmdGen = new RoleCmdGenerator();
            var command = roleCmdGen.UpdateRoleCommand;
            var uriBuilder = new UriBuilder
            {
                Path = $"{RoleApiRoutes.CONTROLLER}/{RoleApiRoutes.PATCH_ROLE.Replace("{roleId}", role.Id.ToString())}",
            };

            using (var content = new StringContent(JsonSerializer.Serialize(command), System.Text.Encoding.UTF8, MimeTypes.Json))
            {
                var patchResponse = await this.client.PatchAsync(uriBuilder.Uri, content);

                Assert.Equal(HttpStatusCode.NoContent, patchResponse.StatusCode);

                // Detach the used entities so we can query it fresh from database.
                this.context.Entry(role).State = EntityState.Detached;
                var roleAfterRequest = await this.context.Roles.FindByIdAsync(role.Id);
                Assert.Equal(command.Name, roleAfterRequest.Name);
            }
        }

        [Fact]
        public async Task UpdateRole_ShouldUpdateJustProvidedPropertiesOfRole_WhenOnlySomePropertyProvided()
        {
            var role = this.context.Roles.First();
            var roleCmdGen = new RoleCmdGenerator();
            var command = roleCmdGen.UpdateRoleCommand;
            var uriBuilder = new UriBuilder
            {
                Path = $"{RoleApiRoutes.CONTROLLER}/{RoleApiRoutes.PATCH_ROLE.Replace("{roleId}", role.Id.ToString())}",
            };

            using (var content = new StringContent(JsonSerializer.Serialize(new { command.ApplicationLevel }), System.Text.Encoding.UTF8, MimeTypes.Json))
            {
                var patchResponse = await this.client.PatchAsync(uriBuilder.Uri, content);

                Assert.Equal(HttpStatusCode.NoContent, patchResponse.StatusCode);

                // Detach the used entities so we can query it fresh from database.
                this.context.Entry(role).State = EntityState.Detached;
                var resourceAfterRequest = await this.context.Roles.FindByIdAsync(role.Id);
                Assert.Equal(role.Name, resourceAfterRequest.Name);
            }
        }

        [Fact]
        public async Task DeleteRole_ShouldDeleteRole_WhenRequestIsValid()
        {
            var role = this.context.Roles.First();
            var uriBuilder = new UriBuilder
            {
                Path = $"{RoleApiRoutes.CONTROLLER}/{RoleApiRoutes.DELETE_ROLE.Replace("{roleId}", role.Id.ToString())}",
            };

            var patchResponse = await this.client.DeleteAsync(uriBuilder.Uri);

            Assert.Equal(HttpStatusCode.NoContent, patchResponse.StatusCode);

            // Detach the used entities so we can query it fresh from database.
            this.context.Entry(role).State = EntityState.Detached;
            var roleAfterRequest = await this.context.Roles.FindByIdAsync(role.Id);
            Assert.Null(roleAfterRequest);
        }

        [Fact]
        public async Task UpdateRolePermissions_ShouldReplaceAllRolePermissions_WhenRequestIsValid()
        {
            var role = this.context.Roles.Include(r => r.RolePermissions).First();
            var id = role.Id;
            var action = this.context.ApplicationResources.Include(c => c.AvailableActions)
                .First(r => r.AvailableActions.Any(a => role.RolePermissions.Select(p => p.Id).Contains(a.Id)))
                .AvailableActions.First(a => role.RolePermissions.Select(p => p.Id).Contains(a.Id));
            var command = new UpdateRolePermissionApplicationCommand
            {
                Permissions = new[] { new PermissionItemApplicationCommandMember { ActionId = action.Id, Permission = Domain.Contracts.Common.Permission.Allow } }
            };

            var uriBuilder = new UriBuilder
            {
                Path = $"{RoleApiRoutes.CONTROLLER}/{RoleApiRoutes.UPDATE_PERMISSIONS.Replace("{roleId}", id.ToString())}",
            };

            var response = await this.client.PutAsJsonAsync(uriBuilder.Uri, command, DefaultResponseJsonSerializerOptions.Options);

            this.context.Entry(role).State = EntityState.Detached;
            role = await this.context.Roles.FindByIdAsync(id, false, r => r.RolePermissions);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Equal(command.Permissions.Count(), role.RolePermissions.Count());
            Assert.All(role.RolePermissions, r => Assert.Contains(command.Permissions, item => item.ActionId == r.ActionId && item.Permission == r.Permission));
        }

        public void Dispose() => this.scope.Dispose();

    }
}
