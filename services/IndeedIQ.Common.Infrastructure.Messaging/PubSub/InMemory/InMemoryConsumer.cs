
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace IndeedIQ.Common.Infrastructure.Messaging.PubSub.InMemory
{
    public class InMemoryConsumer : IConsumer
    {
        private readonly ConcurrentQueue<MessageEnvelope> queue;
        private InMemoryPubSubOptions options;

        public InMemoryConsumer(ConcurrentQueue<MessageEnvelope> queue, InMemoryPubSubOptions options)
        {
            this.queue = queue;
            this.options = options;
        }

        public Task<MessageEnvelope> ConsumeAsync(CancellationToken stoppingToken)
        {
            this.queue.TryDequeue(out MessageEnvelope envelope);
            return Task.FromResult(envelope);
        }

        public void Dispose() { }
    }
}
