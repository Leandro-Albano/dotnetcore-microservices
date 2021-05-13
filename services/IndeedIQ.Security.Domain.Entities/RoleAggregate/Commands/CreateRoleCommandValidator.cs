using FluentValidation;

using IndeedIQ.Common.Domain.Contracts.Exceptions;
using IndeedIQ.Common.Domain.Entities.FluentValidationExtensions;
using IndeedIQ.Security.Domain.Contracts.Common;

using System.Collections.Generic;

namespace IndeedIQ.Security.Domain.Entities.RoleAggregate.Commands
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
