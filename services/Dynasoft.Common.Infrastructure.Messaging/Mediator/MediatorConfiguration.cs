using Microsoft.Extensions.DependencyInjection;

using System;
using System.Linq;


namespace Dynasoft.Common.Infrastructure.Messaging.Mediator
{
    public class MediatorConfiguration
    {
        private readonly IServiceCollection services;

        public MediatorConfiguration(IServiceCollection services) => this.services = services;

        public MediatorConfiguration AddHandler<TMessageHandler>()
            where TMessageHandler : IBaseMessageHandler => this.AddHandler(typeof(TMessageHandler));

        public MediatorConfiguration AddHandler(Type handlerType)
        {
            System.Type baseMessageType = typeof(IBaseMessageHandler);
            System.Type handlerInterface = handlerType.GetInterfaces().First(i => baseMessageType.IsAssignableFrom(i));

            this.services.AddScoped(handlerInterface, handlerType);

            return this;
        }

    }
}
