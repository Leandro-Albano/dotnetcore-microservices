using IndeedIQ.Common.Domain.Entities;
using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Common.Infrastructure.Repositories.EFCore;
using IndeedIQ.Security.Domain.Entities;
using IndeedIQ.Security.Domain.Entities.ResourceAggregate;
using IndeedIQ.Security.Domain.Entities.RoleAggregate;
using IndeedIQ.Security.Domain.Entities.UserAggregate;
using IndeedIQ.Security.Infrastructure.Repositories.Mappings;

using Microsoft.EntityFrameworkCore;

using System;

namespace IndeedIQ.Security.Infrastructure.Repositories
{
    public class SecurityDataContext : ApplicationDataContext, ISecurityDataContext
    {
        public SecurityDataContext(DbContextOptions<SecurityDataContext> options, IMediator mediator) : base(options, mediator) { }

        private IRepository<ApplicationResource> applicationResources;
        public IRepository<ApplicationResource> ApplicationResources => this.applicationResources ??= new Repository<ApplicationResource>(this);

        private IRepository<Role> roles;
        public IRepository<Role> Roles => this.roles ??= new Repository<Role>(this);

        private IRepository<User> users;
        public IRepository<User> Users => this.users ??= new Repository<User>(this);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationResourceMapping());
            modelBuilder.ApplyConfiguration(new ResourceActionMapping());
        }

    }
}
