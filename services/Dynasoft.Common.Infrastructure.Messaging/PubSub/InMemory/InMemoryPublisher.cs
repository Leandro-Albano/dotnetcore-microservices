using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Dynasoft.Common.Infrastructure.Messaging.PubSub.InMemory
{
    public class InMemoryPublisher : IPublisher
    {
        private readonly ConcurrentQueue<MessageEnvelope> queue;

        public InMemoryPublisher(ConcurrentQueue<MessageEnvelope> queue) => this.queue = queue;

        public Task Publish(MessageEnvelope[] messages)
        {
            foreach (var item in messages)
                this.queue.Enqueue(item);

            return Task.CompletedTask;
        }

    }
}
