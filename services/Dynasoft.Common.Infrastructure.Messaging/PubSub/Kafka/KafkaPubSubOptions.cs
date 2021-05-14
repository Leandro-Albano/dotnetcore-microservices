using System.Collections.Generic;

namespace Dynasoft.Common.Infrastructure.Messaging.PubSub.Kafka
{
    public class KafkaPubSubOptions
    {
        public List<string> Servers { get; set; } = new List<string>();
        public List<string> Subscriptions { get; set; } = new List<string>();
        public string ConsumerGroup { get; set; }
        public string TransactionalId { get; set; }
    }
}
