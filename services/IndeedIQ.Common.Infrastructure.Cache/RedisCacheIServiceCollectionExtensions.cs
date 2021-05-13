
using Microsoft.Extensions.DependencyInjection;

using ServiceStack.Redis;

namespace IndeedIQ.Common.Infrastructure.Cache
{
    public static class RedisCacheIServiceCollectionExtensions
    {
        public static void AddRadisCache(this IServiceCollection services, string server)
        {
            services.AddSingleton(scope => new RedisManagerPool(server));
            services.AddSingleton(typeof(ICache<>), typeof(RedisCache<>));
        }
    }
}
