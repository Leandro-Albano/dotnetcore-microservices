using FluentValidation.Results;

using Dynasoft.Common.Domain.Contracts.Exceptions;

using System.Collections.Generic;
using System.Linq;

namespace Dynasoft.Common.Domain.Entities.FluentValidationExtensions
{
    public static class FluentValidationResultToValidationErrorMapper
    {
        public static IEnumerable<ValidationError> ToValidationErros(this ValidationResult validationResult)
        {
            return validationResult.Errors != null && validationResult.Errors.Count > 0
                ? validationResult.Errors.Select(err => new ValidationError
                {
                    Code = FluentValidationErrorCodeToValidationErrorCode.Get(err.ErrorCode),
                    Member = err.PropertyName,
                    Message = err.ErrorMessage
                })
                : Enumerable.Empty<ValidationError>();
        }
    }
}
