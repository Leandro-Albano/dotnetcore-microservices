using Dynasoft.Security.Application.Contracts.DTOs;

using System.Threading.Tasks;

namespace Dynasoft.Security.Application.Client
{
    public interface IAuthClient
    {
        Task<UserDto> AuthenticateAsync(string token);
    }
}
