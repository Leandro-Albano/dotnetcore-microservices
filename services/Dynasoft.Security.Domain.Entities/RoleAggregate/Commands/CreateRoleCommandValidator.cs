using FluentValidation;

using Dynasoft.Common.Domain.Contracts.Exceptions;
using Dynasoft.Common.Domain.Entities.FluentValidationExtensions;
using Dynasoft.Security.Domain.Contracts.Common;

using System.Collections.Generic;

namespace Dynasoft.Security.Domain.Entities.RoleAggregate.Commands
{
    public class CreateRoleCommandValidator : AbstractDomainCommandValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            this.RuleFor(c => c.Name).NotEmpty();
            this.RuleFor(c => c.ApplicationLevel).NotEqual(ApplicationLevel.NotSet);
        }
    }

    public static class CreateRoleCommandExtensions
    {
        private static readonly CreateRoleCommandValidator validator = new CreateRoleCommandValidator();
        public static IEnumerable<ValidationError> Validate(this CreateRoleCommand command, bool throws = false) => validator.ValidateCommand(command, throws);
    }
}
