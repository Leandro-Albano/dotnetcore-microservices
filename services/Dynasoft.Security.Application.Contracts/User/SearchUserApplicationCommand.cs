using Dynasoft.Common.Infrastructure.Messaging.Mediator;
using Dynasoft.Security.Application.Contracts.DTOs;

using System.Collections.Generic;

namespace Dynasoft.Security.Application.Contracts.User
{
    public class SearchUserApplicationCommand : IMessage<IEnumerable<UserDto>>
    {
        public long[] Ids { get; set; }
        public string[] Names { get; set; }
        public bool IncludeRoles { get; set; }
    }
}
