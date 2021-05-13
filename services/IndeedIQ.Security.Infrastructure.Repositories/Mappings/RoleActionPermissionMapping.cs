using IndeedIQ.Common.Infrastructure.Repositories;
using IndeedIQ.Security.Domain.Entities.RoleAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IndeedIQ.Security.Infrastructure.Repositories.Mappings
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
