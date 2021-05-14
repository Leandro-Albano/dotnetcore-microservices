using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Application.Contracts.DTOs;
using Dynasoft.Security.Domain.Contracts.Common;

using System.Collections.Generic;

namespace Dynasoft.Security.Application.Contracts.ApplicationResource
{
    public class SearchApplicationResourcesApplicationCommand : IMessage<IEnumerable<ApplicationResourceDto>>
    {
        public long[] Ids { get; set; }
        public string[] Names { get; set; }
        public bool IncludeActions { get; set; }
        public ApplicationLevel[] Levels { get; set; }
    }
}
