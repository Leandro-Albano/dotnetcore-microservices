using FluentValidation;

using IndeedIQ.Common.Domain.Contracts.Exceptions;
using IndeedIQ.Common.Domain.Entities.FluentValidationExtensions;
using IndeedIQ.Security.Domain.Contracts.Common;

using System.Collections.Generic;

namespace IndeedIQ.Security.Domain.Entities.ResourceAggregate.Commands
{
    public class UpdateApplicationResourceCommandValidator : AbstractDomainCommandValidator<UpdateApplicationResourceCommand>
    {
        public UpdateApplicationResourceCommandValidator()
        {
            this.RuleFor(p => p.Name).NotEmpty();
            this.RuleFor(p => p.ApplicationLevel).NotEqual(ApplicationLevel.NotSet);
        }
    }

    public static class UpdateApplicationResourceCommandExtensions
    {
        private static readonly UpdateApplicationResourceCommandValidator validator = new UpdateApplicationResourceCommandValidator();
        public static IEnumerable<ValidationError> Validate(this UpdateApplicationResourceCommand command, bool throws = false)
            => validator.ValidateCommand(command, throws);
    }
}
