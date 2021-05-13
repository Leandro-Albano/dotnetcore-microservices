using Bogus;

using IndeedIQ.Security.Domain.Entities.UserAggregate.Commands;

using System;

namespace IndeedIQ.Security.Tests.Common
{
    public class UserCmdGenerator
    {
        private readonly Faker<CreateUserCommand> createUserCmdFaker = new Faker<CreateUserCommand>()
            .RuleFor(o => o.Name, f => f.Person.FullName)
            .RuleFor(o => o.Login, f => f.Person.Email)
            .RuleFor(o => o.Email, f => f.Person.Email)
            .RuleFor(o => o.Country, f => f.Address.CountryCode())
            .RuleFor(o => o.Currency, f => f.Finance.Currency().Code)
            .RuleFor(o => o.IndentityServerId, f => Guid.NewGuid().ToString());
        public CreateUserCommand CreateUserCommand => this.createUserCmdFaker.Generate();
    }
}
