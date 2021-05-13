using System.Collections.Generic;

namespace IndeedIQ.Security.Application.Contracts.DTOs
{
    public class UserDto
    {
        public string IdentityServerId { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public IEnumerable<UserRoleDto> Roles { get; set; }
    }

    public class UserRoleDto
    {
        public string RoleName { get; set; }
        public IEnumerable<long> Accounts { get; set; }
        public IEnumerable<long> Orgnisations { get; set; }
    }
}
