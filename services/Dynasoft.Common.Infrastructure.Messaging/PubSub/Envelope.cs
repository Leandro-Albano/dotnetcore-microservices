using System;

namespace Dynasoft.Common.Infrastructure.Messaging.PubSub
{
    public class MessageEnvelope
    {
        public Guid AggregateId { get; set; }
        public string Topic { get; set; }
        public string Indetity { get; set; }
        public virtual object Message { get; set; }
    }

    public class MessageEnvelope<T> : MessageEnvelope
    {
        public new T Message { get; set; }
    }
}
