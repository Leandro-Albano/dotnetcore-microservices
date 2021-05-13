using FluentValidation.Results;

using IndeedIQ.Common.Domain.Contracts.Exceptions;

using System.Collections.Generic;
using System.Linq;

namespace IndeedIQ.Common.Domain.Entities.FluentValidationExtensions
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
