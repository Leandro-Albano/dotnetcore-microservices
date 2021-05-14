
using Dynasoft.Common.Infrastructure.Messaging.Mediator;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Concurrent;

namespace Dynasoft.Common.Infrastructure.Messaging.PubSub.InMemory
{
    public static class InMemoryOptionsBuilderExtensions
    {
        public static PubSubOptionsBuilder UseInMemory(this PubSubOptionsBuilder pubSubOptbuilder, Action<InMemorySubOptionsBuilder> action = null)
        {
            var optionsBuilder = new InMemorySubOptionsBuilder();
            action?.Invoke(optionsBuilder);

            pubSubOptbuilder.Services.AddSingleton<ConcurrentQueue<MessageEnvelope>>();
            pubSubOptbuilder.Services.AddSingleton<IConsumer>(provider =>
            {
                var queue = provider.GetRequiredService<ConcurrentQueue<MessageEnvelope>>();
                return new InMemoryConsumer(queue, optionsBuilder.Options);
            });
            pubSubOptbuilder.Services.AddScoped<IPublisher, InMemoryPublisher>();
            pubSubOptbuilder.Services.AddHostedService(provider =>
            {
                using (var scope = provider.CreateScope())
                {
                    var consumer = scope.ServiceProvider.GetRequiredService<IConsumer>();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    return new ConsumerBackgroundService(consumer, mediator);
                }
            });

            return pubSubOptbuilder;
        }
    }
}
