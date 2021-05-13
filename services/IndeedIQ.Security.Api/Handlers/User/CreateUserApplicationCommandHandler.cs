using AutoMapper;

using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Security.Application.Contracts.DTOs;
using IndeedIQ.Security.Application.Contracts.User;
using IndeedIQ.Security.Domain.Entities;
using IndeedIQ.Security.Domain.Entities.UserAggregate.Commands;

using ServiceStack;

using System.Linq;
using System.Threading.Tasks;

namespace IndeedIQ.Security.Api.Handlers.Role
{
    public class CreateUserApplicationCommandHandler : IMessageHandler<CreateUserApplicationCommand, UserDto>
    {
        private readonly ISecurityDataContext context;
        private readonly IMapper mapper;

        public CreateUserApplicationCommandHandler(ISecurityDataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<UserDto> HandleAsync(CreateUserApplicationCommand message)
        {
            var domainCmd = new CreateUserCommand
            {
                Country = message.Country,
                Currency = message.Currency,
                Email = message.Email,
                Login = message.Login,
                Name = message.Name,
                UserRoles = message.UserRoles?.Select(r => new UserCommandRole
                {
                    GrantedAccounts = r.GrantedAccounts,
                    GrantedOrganisations = r.GrantedOrganisations,
                    Role = this.context.Roles.FindByIdAsync(r.RoleId).ConfigureAwait(false).GetAwaiter().GetResult()
                }).ToArray()
            };

            var user = Domain.Entities.UserAggregate.User.Create(domainCmd);
            await this.context.Users.AddAndPersistAsync(user);

            return this.mapper.Map<UserDto>(user);
        }
    }
}
