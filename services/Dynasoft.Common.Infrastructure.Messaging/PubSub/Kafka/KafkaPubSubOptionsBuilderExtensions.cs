
using Confluent.Kafka;

using Dynasoft.Common.Infrastructure.Messaging.Mediator;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace Dynasoft.Common.Infrastructure.Messaging.PubSub.Kafka
{
    public static class KafkaPubSubOptionsBuilderExtensions
    {
        public static PubSubOptionsBuilder UseKafka(this PubSubOptionsBuilder pubSubOptbuilder, Action<KafkaPubSubOptionsBuilder> action)
        {
            var kafkaOptionsBuilder = new KafkaPubSubOptionsBuilder();
            action?.Invoke(kafkaOptionsBuilder);

            pubSubOptbuilder.Services.AddSingleton(kafkaOptionsBuilder);

            pubSubOptbuilder.Services.AddSingleton<IConsumer>(provider =>
            {
                var config = new ConsumerConfig
                {
                    GroupId = kafkaOptionsBuilder.Options.ConsumerGroup,
                    BootstrapServers = string.Concat(',', kafkaOptionsBuilder.Options.Servers)
                };
                return new KafkaConsumer(config, kafkaOptionsBuilder.Options.Subscriptions);
            });

            pubSubOptbuilder.Services.AddSingleton(provider =>
            {
                return new ProducerConfig
                {
                    BootstrapServers = string.Join(',', kafkaOptionsBuilder.Options.Servers),
                    TransactionalId = kafkaOptionsBuilder.Options.TransactionalId
                };
            });

            pubSubOptbuilder.Services.AddHostedService(provider =>
            {
                using (var scope = provider.CreateScope())
                {
                    var consumer = scope.ServiceProvider.GetRequiredService<IConsumer>();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    return new ConsumerBackgroundService(consumer, mediator);
                }
            });

            pubSubOptbuilder.Services.AddScoped<IPublisher, KafkaPublisher>();

            return pubSubOptbuilder;
        }
    }
}
