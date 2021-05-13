using FluentValidation;

using IndeedIQ.Common.Domain.Contracts.Exceptions;
using IndeedIQ.Common.Domain.Entities.FluentValidationExtensions;

using System.Collections.Generic;

namespace IndeedIQ.Security.Domain.Entities.ResourceAggregate.Commands
{
    public class AddResourceActionCommandValidator : AbstractDomainCommandValidator<AddResourceActionCommand>
    {
        public AddResourceActionCommandValidator() => this.RuleFor(c => c.Name).NotEmpty();
    }

    public static class AddResourceActionCommandExtension
    {
        private static readonly AddResourceActionCommandValidator validator = new AddResourceActionCommandValidator();
        public static IEnumerable<ValidationError> Validate(this AddResourceActionCommand command, bool throws = false)
            => validator.ValidateCommand(command, throws);
    }
}
