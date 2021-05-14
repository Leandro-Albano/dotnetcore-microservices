using Dynasoft.Common.Domain.Contracts.Exceptions;
using Dynasoft.Common.Infrastructure.Messaging.PubSub;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Threading.Tasks;


namespace Dynasoft.Common.Infrastructure.Messaging.Mediator
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider provider;

        private readonly IPublisher publisher;

        public Mediator(IServiceProvider provider, IPublisher publisher)
        {
            this.provider = provider;
            this.publisher = publisher;
        }

        public async Task Send<TMessage>(TMessage command) where TMessage : class
        {
            IMessageHandler<TMessage> service = this.provider.GetRequiredService<IMessageHandler<TMessage>>();
            await service.HandleAsync(command);
        }

        public async Task<TResponse> Send<TMessage, TResponse>(TMessage command) where TMessage : IMessage<TResponse>
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            IMessageHandler<TMessage, TResponse> service = this.provider.GetService<IMessageHandler<TMessage, TResponse>>();

            if (service == null)
                throw new UnexpectedException($"No handler for '{command.GetType().FullName}' returning '{typeof(TResponse).FullName}' found.");

            TResponse result = await service.HandleAsync(command);

            return result;
        }

        public Task Publish(params MessageEnvelope[] message)
            => this.publisher.Publish(message);

    }
}
