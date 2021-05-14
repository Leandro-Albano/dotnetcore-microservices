
using System.Threading.Tasks;

namespace Dynasoft.Common.Infrastructure.Cache
{
    public interface ICache<T>
    {
        Task SaveToCacheAsync(string id, T entity);

        Task<T> GetByIdAsync(string id);
    }
}
