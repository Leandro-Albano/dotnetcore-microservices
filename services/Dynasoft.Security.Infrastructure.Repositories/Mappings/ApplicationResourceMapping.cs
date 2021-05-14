using Dynasoft.Common.Infrastructure.Repositories;
using Dynasoft.Common.Infrastructure.Repositories.EFCore;
using Dynasoft.Security.Domain.Contracts.Common;
using Dynasoft.Security.Domain.Entities.ResourceAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dynasoft.Security.Infrastructure.Repositories.Mappings
{
    public class ApplicationResourceMapping : IApplicationEntityTypeConfiguration<ApplicationResource>
    {
        public void Configure(EntityTypeBuilder<ApplicationResource> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(40).IsRequired();
            builder.Property(p => p.ApplicationLevel)
                .HasConversion(new EnumToStringConverter<ApplicationLevel>())
                .HasMaxLength(40)
                .IsRequired();

            builder
                .HasMany(p => p.AvailableActions)
                .WithOne(p => p.Resource)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata.FindNavigation(nameof(ApplicationResource.AvailableActions)).SetPropertyAccessMode(PropertyAccessMode.Field);


        }
    }
}
