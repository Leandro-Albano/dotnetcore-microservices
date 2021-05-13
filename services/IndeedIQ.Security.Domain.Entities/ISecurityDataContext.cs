using IndeedIQ.Common.Domain.Entities;
using IndeedIQ.Security.Domain.Entities.ResourceAggregate;
using IndeedIQ.Security.Domain.Entities.RoleAggregate;
using IndeedIQ.Security.Domain.Entities.UserAggregate;

namespace IndeedIQ.Security.Domain.Entities
{
    public interface ISecurityDataContext : IDomainDataContext
    {
        IRepository<ApplicationResource> ApplicationResources { get; }
        IRepository<Role> Roles { get; }
        IRepository<User> Users { get; }
    }
}
