using System.Threading.Tasks;

namespace Dynasoft.Common.Infrastructure.Messaging.Mediator
{
    public interface IBaseMessageHandler { }

    public interface IMessageHandler<in TMessage> : IBaseMessageHandler where TMessage : class
    {
        Task HandleAsync(TMessage message);
    }

    public interface IMessageHandler<in TMessage, TResponse> : IBaseMessageHandler
        where TMessage : IMessage<TResponse>
    {
        Task<TResponse> HandleAsync(TMessage message);
    }
}
