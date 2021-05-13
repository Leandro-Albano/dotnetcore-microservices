using Grpc.Core;
using Grpc.Net.Client;

using IndeedIQ.Security.Api.Client;
using IndeedIQ.Security.Application.Contracts.DTOs;

using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IndeedIQ.Security.Application.Client
{
    public class AuthClient : IDisposable, IAuthClient
    {
        private readonly GrpcChannel channel;

        public AuthClient(string authServiceEndpoint)
            => this.channel = GrpcChannel.ForAddress(authServiceEndpoint);

        public async Task<UserDto> AuthenticateAsync(string token)
        {
            var headers = new Metadata
            {
                { nameof(HttpRequestHeader.Authorization), $"Bearer {token}" }
            };

            AuthenticationReply response = null;
            try
            {
                response = await new IndeedIQ.Security.Api.Client.Auth.AuthClient(this.channel)
                    .AuthenticateAsync(new AuthenticationRequest(), headers);
            }
            catch (Grpc.Core.RpcException ex)
            {
                throw new AuthException(GrpcStatusCodeToHttpStatusCode.Map[ex.StatusCode], ex.Message, ex);
            }
            return new UserDto
            {
                IdentityServerId = response.IdentityServerId,
                Name = response.Name,
                Email = response.Email,
                Login = response.Login,
                Country = response.Country,
                Currency = response.Currency,
                Roles = response.Roles.Select(r => new UserRoleDto
                {
                    RoleName = r.RoleName,
                    Accounts = r.Accounts.ToArray(),
                    Orgnisations = r.Organisations.ToArray()
                }),
            };
        }

        public void Dispose() => this.channel.Dispose();
    }
}
