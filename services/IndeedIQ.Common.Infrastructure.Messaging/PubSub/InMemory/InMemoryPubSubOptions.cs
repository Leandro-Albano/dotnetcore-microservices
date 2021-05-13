
using System.Collections.Generic;

namespace IndeedIQ.Common.Infrastructure.Messaging.PubSub.InMemory
{
    public class InMemoryPubSubOptions
    {
        public List<string> Subscriptions { get; set; } = new List<string>();
    }
}
