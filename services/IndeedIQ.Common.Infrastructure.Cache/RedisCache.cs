
using ServiceStack.Redis;

using System.Threading.Tasks;

namespace IndeedIQ.Common.Infrastructure.Cache
{
    public class RedisCache<T> : ICache<T>
    {
        private readonly IRedisClient client;

        public RedisCache(RedisManagerPool redisManager) => this.client = redisManager.GetClient();

        public Task<T> GetByIdAsync(string id) => Task.FromResult(this.client.Get<T>(id));

        public Task SaveToCacheAsync(string id, T entity)
        {
            this.client.Add(id, entity);
            return Task.CompletedTask;
        }
    }
}
