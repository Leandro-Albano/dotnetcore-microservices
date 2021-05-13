﻿using IndeedIQ.Common.Domain.Contracts;
using IndeedIQ.Security.Domain.Contracts.Common;

namespace IndeedIQ.Security.Domain.Entities.RoleAggregate.Commands
{
    public class CreateRoleCommand : IDomainCommand
    {
        public string Name { get; set; }
        public ApplicationLevel ApplicationLevel { get; set; }
    }
}
