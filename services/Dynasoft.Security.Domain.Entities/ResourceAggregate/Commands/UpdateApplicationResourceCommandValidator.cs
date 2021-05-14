using FluentValidation;

using Dynasoft.Common.Domain.Contracts.Exceptions;
using Dynasoft.Common.Domain.Entities.FluentValidationExtensions;
using Dynasoft.Security.Domain.Contracts.Common;

using System.Collections.Generic;

namespace Dynasoft.Security.Domain.Entities.ResourceAggregate.Commands
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
