using Dynasoft.Common.Infrastructure.Repositories;
using Dynasoft.Common.Infrastructure.Repositories.EFCore;
using Dynasoft.Security.Domain.Entities.UserAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dynasoft.Security.Infrastructure.Repositories.Mappings
{
    public class UserMapping : IApplicationEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Login).HasMaxLength(150).IsRequired();
            builder.Property(p => p.Country).HasMaxLength(2).IsRequired();
            builder.Property(p => p.Currency).HasMaxLength(3).IsRequired();

            builder.HasMany(p => p.Roles).WithOne(p => p.User).OnDelete(DeleteBehavior.Cascade);

            builder.Metadata.FindNavigation(nameof(User.Roles)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
