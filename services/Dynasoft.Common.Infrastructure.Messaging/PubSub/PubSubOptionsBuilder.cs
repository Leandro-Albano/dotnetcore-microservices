using Microsoft.Extensions.DependencyInjection;

namespace Dynasoft.Common.Infrastructure.Messaging.PubSub
{
    public class PubSubOptionsBuilder
    {
        internal IServiceCollection Services { get; }

        public PubSubOptionsBuilder(IServiceCollection services) => this.Services = services;
    }
}
