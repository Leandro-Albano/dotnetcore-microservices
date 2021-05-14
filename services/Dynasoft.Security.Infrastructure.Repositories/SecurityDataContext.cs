using Dynasoft.Common.Domain.Entities;
using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Common.Infrastructure.Repositories.EFCore;
using Dynasoft.Security.Domain.Entities;
using Dynasoft.Security.Domain.Entities.ResourceAggregate;
using Dynasoft.Security.Domain.Entities.RoleAggregate;
using Dynasoft.Security.Domain.Entities.UserAggregate;
using Dynasoft.Security.Infrastructure.Repositories.Mappings;

using Microsoft.EntityFrameworkCore;

using System;

namespace Dynasoft.Security.Infrastructure.Repositories
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
