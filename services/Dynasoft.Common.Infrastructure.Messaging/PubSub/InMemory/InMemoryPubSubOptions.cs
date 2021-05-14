
using System.Collections.Generic;

namespace Dynasoft.Common.Infrastructure.Messaging.PubSub.InMemory
{
    public class InMemoryPubSubOptions
    {
        public List<string> Subscriptions { get; set; } = new List<string>();
    }
}
