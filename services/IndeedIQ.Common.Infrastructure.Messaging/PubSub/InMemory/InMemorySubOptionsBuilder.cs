namespace IndeedIQ.Common.Infrastructure.Messaging.PubSub.InMemory
{
    public class InMemorySubOptionsBuilder
    {
        public InMemoryPubSubOptions Options { get; } = new InMemoryPubSubOptions();

        public InMemorySubOptionsBuilder Subscribe(string topic)
        {
            this.Options.Subscriptions.Add(topic);
            return this;
        }

    }
}
