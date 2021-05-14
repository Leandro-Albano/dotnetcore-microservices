using FluentValidation;

using Dynasoft.Common.Domain.Contracts.Exceptions;
using Dynasoft.Common.Domain.Entities.FluentValidationExtensions;
using Dynasoft.Security.Domain.Contracts.Common;

using System.Collections.Generic;

namespace Dynasoft.Security.Domain.Entities.RoleAggregate.Commands
{
    public class UpdateRoleCommandValidator : AbstractDomainCommandValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            this.RuleFor(c => c.Name).NotEmpty();
            this.RuleFor(c => c.ApplicationLevel).NotEqual(ApplicationLevel.NotSet);
        }
    }

    public static class UpdateRoleCommandExtensions
    {
        private static readonly UpdateRoleCommandValidator validator = new UpdateRoleCommandValidator();
        public static IEnumerable<ValidationError> Validate(this UpdateRoleCommand command, bool throws = false) => validator.ValidateCommand(command, throws);
    }
}
