namespace Dynasoft.Common.Domain.Contracts.Exceptions
{
    public class ValidationErrorCode
    {
        public const string MissingRequiredMember = nameof(ValidationErrorCode.MissingRequiredMember);
        public const string MaxLengh = nameof(ValidationErrorCode.MaxLengh);
        public const string MinLengh = nameof(ValidationErrorCode.MinLengh);
        public const string InvalidFormat = nameof(ValidationErrorCode.InvalidFormat);
        public const string InvalidReferece = nameof(ValidationErrorCode.InvalidReferece);
        public const string InvalidGuid = nameof(ValidationErrorCode.InvalidGuid);
        public const string InvalidValue = nameof(ValidationErrorCode.InvalidValue);
        public const string GreaterThan = nameof(ValidationErrorCode.GreaterThan);
    }
}
