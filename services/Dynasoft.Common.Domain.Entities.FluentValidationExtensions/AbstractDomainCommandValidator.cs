using FluentValidation;

using Dynasoft.Common.Domain.Contracts;
using Dynasoft.Common.Domain.Contracts.Exceptions;

using System.Collections.Generic;
using System.Linq;

namespace Dynasoft.Common.Domain.Entities.FluentValidationExtensions
{

    /// <summary>
    /// Base class for domain command validators using fluent validator.
    /// </summary>
    /// <typeparam name="T">The command being validated.</typeparam>
    public abstract class AbstractDomainCommandValidator<T> : AbstractValidator<T> where T : IDomainCommand
    {
        public IEnumerable<ValidationError> ValidateCommand(T command, bool throws = false)
        {
            IEnumerable<ValidationError> errors = this.Validate(command).ToValidationErros();

            return errors.Any() && throws
                ? throw new DomainValidationException(typeof(T).Name, errors)
                : errors;
        }

        public IEnumerable<ValidationError> ValidateCommand(ValidationContext<T> context, bool throws = false)
        {
            IEnumerable<ValidationError> errors = this.Validate(context).ToValidationErros();

            return errors.Any() && throws
                ? throw new DomainValidationException(typeof(T).Name, errors)
                : errors;
        }
    }
}
