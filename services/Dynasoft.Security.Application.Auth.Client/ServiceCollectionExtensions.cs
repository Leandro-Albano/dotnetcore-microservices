
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Dynasoft.Security.Application.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, string authServiceEndpoint)
        {
            services
                .AddAuthentication(SecutiryServiceAuthenticationSchemes.RemoteAuth)
                .AddScheme<AuthenticationSchemeOptions, IIQCustomAuthenticationHandler>(SecutiryServiceAuthenticationSchemes.RemoteAuth, o => { });

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddSingleton<IAuthClient>(new AuthClient(authServiceEndpoint));

            return services;
        }
    }
}
