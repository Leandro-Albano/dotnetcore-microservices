using Dynasoft.Common.Infrastructure.Repositories;
using Dynasoft.Common.Infrastructure.Repositories.EFCore;
using Dynasoft.Security.Domain.Entities.ResourceAggregate;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dynasoft.Security.Infrastructure.Repositories.Mappings
{
    public class ResourceActionMapping : IApplicationEntityTypeConfiguration<ResourceAction>
    {
        public void Configure(EntityTypeBuilder<ResourceAction> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                .HasMaxLength(30)
                .IsRequired();
            builder.HasOne(p => p.Resource).WithMany(p => p.AvailableActions).IsRequired();
            builder.HasOne(p => p.Resource);
        }
    }
}
