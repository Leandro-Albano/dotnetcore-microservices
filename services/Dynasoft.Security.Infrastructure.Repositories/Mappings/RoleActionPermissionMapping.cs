using Dynasoft.Common.Infrastructure.Repositories;
using Dynasoft.Common.Infrastructure.Repositories.EFCore;
using Dynasoft.Security.Domain.Entities.RoleAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dynasoft.Security.Infrastructure.Repositories.Mappings
{
    public class RoleActionPermissionMapping : IApplicationEntityTypeConfiguration<RoleActionPermission>
    {
        public void Configure(EntityTypeBuilder<RoleActionPermission> builder)
        {
            builder.HasOne(p => p.Action)
                .WithMany().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
