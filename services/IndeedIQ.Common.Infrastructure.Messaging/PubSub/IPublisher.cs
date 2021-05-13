using System.Threading.Tasks;

namespace IndeedIQ.Common.Infrastructure.Messaging.PubSub
{
    public interface IPublisher
    {
        Task Publish(MessageEnvelope[] message);
    }
}
