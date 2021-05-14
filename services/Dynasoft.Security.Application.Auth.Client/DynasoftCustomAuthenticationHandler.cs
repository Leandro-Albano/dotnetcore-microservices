using Dynasoft.Security.Application.Contracts.DTOs;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Dynasoft.Security.Application.Client
{
    public class IIQCustomAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthClient authClient;

        public IIQCustomAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock, IAuthClient authClient) :
            base(options, logger, encoder, clock) => this.authClient = authClient;

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!this.Request.Headers.TryGetValue(nameof(HttpRequestHeader.Authorization), out var header)
                || !AuthenticationHeaderValue.TryParse(header, out var headerValue)
                || !string.Equals(headerValue.Scheme, "Bearer", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Invalid Bearer authentication header");
            }

            UserDto user = null;
            try
            { user = await this.authClient.AuthenticateAsync(headerValue.Parameter); }
            catch (AuthException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            { return AuthenticateResult.Fail("Invalid Bearer token"); }

            var identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, CustomClaimTypes.Indentity),
                new Claim(ClaimTypes.NameIdentifier, user.IdentityServerId),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Country, user.Country),
                new Claim(CustomClaims.Currency, user.Currency)
            }, this.Scheme.Name);

            var roleIdentities = user.Roles.Select(r =>
            {
                return new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, r.RoleName),
                    new Claim(ClaimTypes.Role, r.RoleName),
                    new Claim(CustomClaims.Accounts, string.Join(",", r.Accounts?.Select(a => a.ToString()) ?? new string[]{ })),
                    new Claim(CustomClaims.Organisations, string.Join(",", r.Orgnisations?.Select(a => a.ToString()) ?? new string[]{ })),
                    new Claim(CustomClaims.RoleActionPermission, string.Join(",", r.Orgnisations?.Select(a => a.ToString()) ?? new string[]{ })),
                }, this.Scheme.Name);
            });

            var principal = new ClaimsPrincipal(Enumerable.Concat(new[] { identity }, roleIdentities));
            var ticket = new AuthenticationTicket(principal, this.Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

    }

    public static class IIQSecurityServiceSchemes
    {
        public const string Bearer = nameof(Bearer);
    }

    public static class CustomClaims
    {
        public const string Accounts = nameof(Accounts);
        public const string Organisations = nameof(Organisations);
        public const string Currency = nameof(Currency);
        public const string RoleActionPermission = nameof(RoleActionPermission);
    }

    public static class CustomClaimTypes
    {
        public const string Indentity = nameof(Indentity);
    }
}
