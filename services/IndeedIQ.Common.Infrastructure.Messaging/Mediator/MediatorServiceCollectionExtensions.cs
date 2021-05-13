using Microsoft.Extensions.DependencyInjection;

using System;
using System.Linq;
using System.Reflection;

namespace IndeedIQ.Common.Infrastructure.Messaging.Mediator
{
    public static class MediatorServiceCollectionExtensions
    {
        public static void AddMediator(this IServiceCollection services, params Assembly[] assemblies)
        {
            var messageHandlerType = typeof(IBaseMessageHandler);
            var handlers = assemblies.SelectMany(a => a.DefinedTypes).Where(t => messageHandlerType.IsAssignableFrom(t));

            AddMediator(services, config =>
            {
                foreach (var handler in handlers)
                    config.AddHandler(handler);
            });
        }

        public static void AddMediator(this IServiceCollection services, Action<MediatorConfiguration> action = null)
        {
            if (action != null)
            {
                MediatorConfiguration config = new MediatorConfiguration(services);
                action(config);
            }

            services.AddScoped<IMediator, Mediator>();
        }
    }

}
