using System;
using System.Threading;
using System.Threading.Tasks;

namespace IndeedIQ.Common.Infrastructure.Messaging.PubSub
{
    public interface IConsumer : IDisposable
    {
        Task<MessageEnvelope> ConsumeAsync(CancellationToken stoppingToken);
    }
}
