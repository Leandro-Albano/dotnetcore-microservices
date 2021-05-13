using FluentValidation;

using IndeedIQ.Common.Domain.Contracts.Exceptions;
using IndeedIQ.Common.Domain.Entities.FluentValidationExtensions;
using IndeedIQ.Security.Domain.Contracts.Common;

using System.Collections.Generic;

namespace IndeedIQ.Security.Domain.Entities.RoleAggregate.Commands
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
