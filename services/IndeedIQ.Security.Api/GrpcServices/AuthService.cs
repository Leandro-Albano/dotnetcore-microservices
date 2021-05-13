using Grpc.Core;

using IndeedIQ.Security.Domain.Entities;
using IndeedIQ.Security.Domain.Entities.UserAggregate;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

using System.Linq;
using System.Threading.Tasks;

namespace IndeedIQ.Security.Api.GrpcServices
{
    public class AuthService : Auth.AuthBase
    {
        private readonly ILogger<AuthService> logger;
        private readonly ISecurityDataContext dbContext;

        public AuthService(ILogger<AuthService> logger, ISecurityDataContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public override Task<AuthorisationReply> IsAuthorised(AuthorisationRequest request, ServerCallContext context)
        {
            return Task.FromResult(new AuthorisationReply
            {
                IsAuthorised = false
            });
        }

        [Authorize]
        public override Task<AuthenticationReply> Authenticate(AuthenticationRequest request, ServerCallContext context)
        {
            var identityServerId = context.GetHttpContext().User.FindFirst(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = this.dbContext.Users.AsReadOnly()
                .Include(u => u.Roles)
                    .ThenInclude(u => u.Role)
                        .ThenInclude(r => r.RolePermissions)
                            .ThenInclude(r => r.Action)
                                .ThenInclude(r => r.Resource)
                .FindByIdentityServerId(identityServerId);

            return Task.FromResult(new AuthenticationReply
            {
                IdentityServerId = user.IdentityServerId,
                Name = user.Name,
                Country = user.Country,
                Currency = user.Currency,
                Email = user.Email,
                Roles = { user.Roles.Select(r => new AuthenticationReplyUserRole
                {
                    RoleName = r.Role.Name,
                    Accounts = { r.Accounts },
                    Organisations = { r.Organisations },
                    AuthorisedActions = { r.Role.RolePermissions.Select(p=>p.Action.FullName) }
                }) }
            });
        }
    }
}
