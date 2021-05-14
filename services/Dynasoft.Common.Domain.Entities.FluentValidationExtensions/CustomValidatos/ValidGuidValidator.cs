using FluentValidation;

using System;

namespace Dynasoft.Common.Domain.Entities.FluentValidationExtensions.CustomValidatos
{
    public static class ValidGuidValidator
    {
        public static IRuleBuilderOptions<T, Guid> IsValidGuid<T>(this IRuleBuilderInitial<T, Guid> ruleBuilder)
        {
            return ruleBuilder.Must(val => val != Guid.Empty)
                .WithErrorCode(nameof(ValidGuidValidator));
        }
    }
}
