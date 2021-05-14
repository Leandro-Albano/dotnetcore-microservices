using Dynasoft.Common.Domain.Entities;
using Dynasoft.Security.Domain.Entities.ResourceAggregate;
using Dynasoft.Security.Domain.Entities.RoleAggregate;
using Dynasoft.Security.Domain.Entities.UserAggregate;

namespace Dynasoft.Security.Domain.Entities
{
    public interface ISecurityDataContext : IDomainDataContext
    {
        IRepository<ApplicationResource> ApplicationResources { get; }
        IRepository<Role> Roles { get; }
        IRepository<User> Users { get; }
    }
}
