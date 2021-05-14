using Dynasoft.Common.Infrastructure.Messaging.Mediator;

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dynasoft.Security.Application.Contracts.Role
{
    public class UpdateRoleApplicationCommand : IMessage
    {
        public long Id { get; set; }
        [JsonExtensionData] public IDictionary<string, object> ExtraProperties { get; set; }
    }
}
