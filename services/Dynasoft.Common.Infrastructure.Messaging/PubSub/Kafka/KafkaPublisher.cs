using Confluent.Kafka;

using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dynasoft.Common.Infrastructure.Messaging.PubSub.Kafka
{
    public class KafkaPublisher : IPublisher
    {
        private readonly ProducerConfig producerConfig;

        public KafkaPublisher(ProducerConfig producerConfig) => this.producerConfig = producerConfig;

        public async Task Publish(MessageEnvelope[] messages)
        {
            using IProducer<string, string> producer = new ProducerBuilder<string, string>(this.producerConfig).Build();

            try
            {
                producer.InitTransactions(TimeSpan.FromSeconds(5));
                producer.BeginTransaction();

                foreach (var item in messages)
                {
                    string serializedMsg = JsonSerializer.Serialize(item);
                    await producer.ProduceAsync(item.Topic, new Message<string, string>() { Key = null, Value = serializedMsg });

                    producer.CommitTransaction(TimeSpan.FromSeconds(3));
                }
            }
            catch (Exception)
            {
                producer.AbortTransaction(TimeSpan.FromSeconds(3));
                throw;
            }
        }

    }
}
