using FluentValidation;

using Dynasoft.Common.Domain.Contracts.Exceptions;
using Dynasoft.Common.Domain.Entities.FluentValidationExtensions;
using Dynasoft.Security.Domain.Contracts.Common;
using Dynasoft.Security.Domain.Entities.Validation;

using System.Collections.Generic;
using System.Linq;

namespace Dynasoft.Security.Domain.Entities.UserAggregate.Commands
{
    public class CreateUserCommandValidator : AbstractDomainCommandValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            this.RuleFor(c => c.Name).NotEmpty();
            this.RuleFor(c => c.Email).NotEmpty().EmailAddress();
            this.RuleFor(c => c.Country).NotEmpty();
            this.RuleFor(c => c.Currency).NotEmpty();
            this.RuleFor(c => c.Login).NotEmpty();

            // The user must have at least one role
            this.RuleFor(c => c.UserRoles).NotEmpty().ForEach(item => item.NotNull());
            this.RuleForEach(c => c.UserRoles).ChildRules(item =>
            {
                item.RuleFor(c => c).NotNull();
                item.RuleFor(c => c.Role).NotNull();

                // When it's an account level user, we need to check if it has at least one account id granted
                item.RuleForEach(c => c.GrantedAccounts).ChildRules(item => item.RuleFor(c => c).GreaterThan(0));
                item.RuleFor(c => c.GrantedAccounts).NotEmpty()
                    .When(c => c.Role != null && c.Role.RolePermissions.Any(p => p.Action.Resource.ApplicationLevel == ApplicationLevel.Account))
                    .WithErrorCode(SecurityDomainValidationErrorCode.NoGrantedAccountsProvided);

                // When it's an organisation level user, we need to check if it has at least one organisation id granted
                item.RuleForEach(c => c.GrantedOrganisations).ChildRules(item => item.RuleFor(c => c).GreaterThan(0));
                item.RuleFor(c => c.GrantedOrganisations).NotEmpty()
                    .When(c => c.Role != null && c.Role.RolePermissions.Any(p => p.Action.Resource.ApplicationLevel == ApplicationLevel.Organisation))
                    .WithErrorCode(SecurityDomainValidationErrorCode.NoGrantedOrganisationsProvided);
            });
        }
    }

    public static class CreateUserCommandExtensions
    {
        private static readonly CreateUserCommandValidator validator = new CreateUserCommandValidator();
        public static IEnumerable<ValidationError> Validate(this CreateUserCommand command, bool throws = false)
            => validator.ValidateCommand(command, throws);
    }
}
