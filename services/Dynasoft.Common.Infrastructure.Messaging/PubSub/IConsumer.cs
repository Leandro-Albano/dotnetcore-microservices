using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dynasoft.Common.Infrastructure.Messaging.PubSub
{
    public interface IConsumer : IDisposable
    {
        Task<MessageEnvelope> ConsumeAsync(CancellationToken stoppingToken);
    }
}
