using FluentValidation;

using Dynasoft.Common.Domain.Contracts.Exceptions;
using Dynasoft.Common.Domain.Entities.FluentValidationExtensions;

using System.Collections.Generic;

namespace Dynasoft.Security.Domain.Entities.ResourceAggregate.Commands
{
    public class CreateApplicationResourceCommandValidator : AbstractDomainCommandValidator<CreateApplicationResourceCommand>
    {
        public CreateApplicationResourceCommandValidator()
        {
            this.RuleFor(c => c.Name).NotEmpty();
            this.RuleFor(c => c.ApplicationLevel).NotEqual(Contracts.Common.ApplicationLevel.NotSet);
        }
    }

    public static class CreateApplicationResourceCommandExtensions
    {
        private static readonly CreateApplicationResourceCommandValidator validatior = new CreateApplicationResourceCommandValidator();
        public static IEnumerable<ValidationError> Validate(this CreateApplicationResourceCommand command, bool throws = false)
            => validatior.ValidateCommand(command, throws);
    }
}
