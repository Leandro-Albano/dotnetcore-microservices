using System.Collections.Generic;

namespace Dynasoft.Common.Domain.Contracts.Exceptions
{
    public class DomainValidationException : DomainException
    {

        public DomainValidationException(string commandName, IEnumerable<ValidationError> validationErrors)
            : base(ExceptionCode.ValidationException, null, DomainMessages.INVALID_COMMAND, commandName) => this.ValidationErrors = validationErrors;

        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}
