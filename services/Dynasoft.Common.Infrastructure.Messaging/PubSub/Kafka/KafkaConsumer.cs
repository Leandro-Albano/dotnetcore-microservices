using Confluent.Kafka;

using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Dynasoft.Common.Infrastructure.Messaging.PubSub.Kafka
{
    public class KafkaConsumer : IConsumer
    {
        private readonly IConsumer<string, string> consumer;

        public KafkaConsumer(ConsumerConfig consumerConfig, IEnumerable<string> substractions)
        {
            this.consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
            this.consumer.Subscribe(substractions);
        }

        public Task<MessageEnvelope> ConsumeAsync(CancellationToken stoppingToken)
        {
            ConsumeResult<string, string> response = this.consumer.Consume(stoppingToken);
            if (response.IsPartitionEOF)
                return null;

            MessageEnvelope envelope = JsonSerializer.Deserialize<MessageEnvelope>(response.Message.Value);
            return Task.FromResult(envelope);
        }

        public void Dispose() => this.consumer.Dispose();
    }
}
