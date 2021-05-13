using IndeedIQ.Security.Application.Contracts.DTOs;

using System.Threading.Tasks;

namespace IndeedIQ.Security.Application.Client
{
    public interface IAuthClient
    {
        Task<UserDto> AuthenticateAsync(string token);
    }
}
