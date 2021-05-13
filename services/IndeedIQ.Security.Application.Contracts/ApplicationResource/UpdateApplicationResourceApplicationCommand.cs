using IndeedIQ.Common.Infrastructure.Messaging.Mediator;

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IndeedIQ.Security.Application.Contracts.ApplicationResource
{
    public class UpdateApplicationResourceApplicationCommand : IMessage
    {
        public long Id { get; set; }
        [JsonExtensionData] public IDictionary<string, object> ExtraProperties { get; set; }
    }
}
