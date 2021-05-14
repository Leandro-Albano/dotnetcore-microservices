
using Microsoft.Extensions.DependencyInjection;

using System;

namespace Dynasoft.Common.Infrastructure.Messaging.PubSub
{
    public static class PubSubIServiceCollectionExtensions
    {
        public static void AddPubSub(this IServiceCollection services, Action<PubSubOptionsBuilder> action)
        {
            var builder = new PubSubOptionsBuilder(services);
            action?.Invoke(builder);
        }
    }
}
