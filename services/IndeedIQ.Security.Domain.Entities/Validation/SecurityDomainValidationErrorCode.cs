using IndeedIQ.Common.Domain.Contracts.Exceptions;

namespace IndeedIQ.Security.Domain.Entities.Validation
{
    public class SecurityDomainValidationErrorCode : ValidationErrorCode
    {
        public const string InconsistentRoleActionApplicationLevel = nameof(InconsistentRoleActionApplicationLevel);
        public const string NoGrantedAccountsProvided = nameof(NoGrantedAccountsProvided);
        public const string NoGrantedOrganisationsProvided = nameof(NoGrantedOrganisationsProvided);
    }
}
