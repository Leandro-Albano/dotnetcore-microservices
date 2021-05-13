using IndeedIQ.Security.Domain.Entities.UserAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Linq;

namespace IndeedIQ.Security.Infrastructure.Repositories.Mappings
{
    public class UserRoleMapping : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.Property(p => p.Accounts).HasConversion(v => string.Join(",", v),
                                                            v => v.Split(',', StringSplitOptions.None).Select(long.Parse).ToList());

            builder.Property(p => p.Organisations).HasConversion(v => string.Join(",", v),
                                                                 v => v.Split(',', StringSplitOptions.None).Select(long.Parse).ToList());
        }
    }
}
