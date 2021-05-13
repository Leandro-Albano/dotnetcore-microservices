
using System.Threading.Tasks;

namespace IndeedIQ.Common.Infrastructure.Cache
{
    public interface ICache<T>
    {
        Task SaveToCacheAsync(string id, T entity);

        Task<T> GetByIdAsync(string id);
    }
}
