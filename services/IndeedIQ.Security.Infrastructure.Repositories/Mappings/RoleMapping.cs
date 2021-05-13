using IndeedIQ.Common.Infrastructure.Repositories;
using IndeedIQ.Security.Domain.Entities.RoleAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IndeedIQ.Security.Infrastructure.Repositories.Mappings
{
    public class RoleMapping : IApplicationEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(p => p.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder
                .HasMany(p => p.RolePermissions)
                .WithOne(p => p.Role)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata.FindNavigation(nameof(Role.RolePermissions)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
