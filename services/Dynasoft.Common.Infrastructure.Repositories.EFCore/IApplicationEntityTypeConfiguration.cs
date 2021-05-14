
using Microsoft.EntityFrameworkCore;

namespace Dynasoft.Common.Infrastructure.Repositories.EFCore
{
    public interface IApplicationEntityTypeConfiguration
    {
    }

    public interface IApplicationEntityTypeConfiguration<T> : IApplicationEntityTypeConfiguration, IEntityTypeConfiguration<T>
        where T : class
    {
    }
}
