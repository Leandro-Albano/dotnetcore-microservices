using FluentValidation.Validators;

using Dynasoft.Common.Domain.Contracts.Exceptions;
using Dynasoft.Common.Domain.Entities.FluentValidationExtensions.CustomValidatos;

using System;
using System.Collections.Generic;

namespace Dynasoft.Common.Domain.Entities.FluentValidationExtensions
{
    internal sealed class FluentValidationErrorCodeToValidationErrorCode
    {
        private static readonly Dictionary<string, string> Map = new Dictionary<string, string>
        {
            { nameof(NotNullValidator<object, object>), ValidationErrorCode.MissingRequiredMember },
            { nameof(NotEmptyValidator<object, object>), ValidationErrorCode.MissingRequiredMember },
            { nameof(ValidGuidValidator), ValidationErrorCode.InvalidGuid },
            { nameof(NotEqualValidator<object, object>), ValidationErrorCode.InvalidValue },
            { nameof(GreaterThanValidator<object, string>), ValidationErrorCode.GreaterThan }
        };

        public static string Get(string errorCode)
        {
            Map.TryGetValue(errorCode, out var value);
            return value ?? errorCode;
        }
    }
}
