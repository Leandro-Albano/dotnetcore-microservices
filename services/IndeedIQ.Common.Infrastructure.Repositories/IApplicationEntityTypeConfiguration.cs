using Microsoft.EntityFrameworkCore;

namespace IndeedIQ.Common.Infrastructure.Repositories
{
    public interface IApplicationEntityTypeConfiguration
    {
    }

    public interface IApplicationEntityTypeConfiguration<T> : IApplicationEntityTypeConfiguration, IEntityTypeConfiguration<T>
        where T : class
    {
    }
}
