using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Security.Application.Contracts.DTOs;
using IndeedIQ.Security.Domain.Contracts.Common;

using System.Collections.Generic;

namespace IndeedIQ.Security.Application.Contracts.ApplicationResource
{
    public class SearchApplicationResourcesApplicationCommand : IMessage<IEnumerable<ApplicationResourceDto>>
    {
        public long[] Ids { get; set; }
        public string[] Names { get; set; }
        public bool IncludeActions { get; set; }
        public ApplicationLevel[] Levels { get; set; }
    }
}
