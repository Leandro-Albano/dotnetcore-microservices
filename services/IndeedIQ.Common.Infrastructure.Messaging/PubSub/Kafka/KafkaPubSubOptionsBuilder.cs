
using System;
using System.Diagnostics.CodeAnalysis;

namespace IndeedIQ.Common.Infrastructure.Messaging.PubSub.Kafka
{
    public class KafkaPubSubOptionsBuilder
    {
        public KafkaPubSubOptions Options { get; } = new KafkaPubSubOptions();

        public KafkaPubSubOptionsBuilder SetTransationId([NotNull] string transactionId)
        {
            if (string.IsNullOrEmpty(transactionId))
                throw new ArgumentNullException(nameof(transactionId));

            this.Options.TransactionalId = transactionId;

            return this;
        }

        public KafkaPubSubOptionsBuilder AddServers(params string[] servers)
        {
            this.Options.Servers.AddRange(servers);
            return this;
        }

        public KafkaPubSubOptionsBuilder Subscribe(params string[] topic)
        {
            this.Options.Subscriptions.AddRange(topic);
            return this;
        }

        public KafkaPubSubOptionsBuilder SetConsumerGroup(string consumerGroup)
        {
            this.Options.ConsumerGroup = consumerGroup;
            return this;
        }
    }
}
