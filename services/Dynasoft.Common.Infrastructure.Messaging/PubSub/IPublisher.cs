using System.Threading.Tasks;

namespace Dynasoft.Common.Infrastructure.Messaging.PubSub
{
    public interface IPublisher
    {
        Task Publish(MessageEnvelope[] message);
    }
}
