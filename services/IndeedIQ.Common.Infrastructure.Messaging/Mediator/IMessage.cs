
namespace IndeedIQ.Common.Infrastructure.Messaging.Mediator
{
    public interface IMessage { }

    public interface IMessage<out TResponse> : IMessage { }

}
