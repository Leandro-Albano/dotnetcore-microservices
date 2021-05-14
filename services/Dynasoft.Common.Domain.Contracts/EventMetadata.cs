using System;

namespace Dynasoft.Common.Domain.Contracts
{
    public class EventMetadata
    {
        public string EventName { get; set; }
        public int EventVersion { get; set; } = 1;
        public DateTime EventRaisedAt { get; set; } = DateTime.UtcNow;
    }
}
