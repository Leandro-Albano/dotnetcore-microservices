
using IndeedIQ.Common.Infrastructure.Messaging.Mediator;

using Microsoft.Extensions.Hosting;

using System.Threading;
using System.Threading.Tasks;

namespace IndeedIQ.Common.Infrastructure.Messaging.PubSub
{
    public class ConsumerBackgroundService : BackgroundService
    {
        private readonly IMediator mediator;
        private readonly IConsumer consumer;

        public ConsumerBackgroundService(IConsumer consumer, IMediator mediator)
        {
            this.consumer = consumer;
            this.mediator = mediator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var envelope = await this.consumer.ConsumeAsync(stoppingToken);
                    if (envelope != null)
                        await this.mediator.Send(envelope.Message);
                }
            });
        }

        public override void Dispose()
        {
            this.consumer.Dispose();
            base.Dispose();
        }
    }
}
