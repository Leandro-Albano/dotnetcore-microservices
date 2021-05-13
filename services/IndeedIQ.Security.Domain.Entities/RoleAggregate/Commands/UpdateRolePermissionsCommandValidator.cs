
using FluentValidation;

using IndeedIQ.Common.Domain.Contracts.Exceptions;
using IndeedIQ.Common.Domain.Entities.FluentValidationExtensions;

using System.Collections.Generic;
using System.Linq;

namespace IndeedIQ.Security.Domain.Entities.RoleAggregate.Commands
{
    public class UpdateRolePermissionsCommandValidator : AbstractDomainCommandValidator<UpdateRolePermissionsCommand>
    {
        public UpdateRolePermissionsCommandValidator()
        {
            this.RuleFor(c => c.Permissions).Must(c => c.Any());
            this.RuleForEach(c => c.Permissions).ChildRules(item => item.RuleFor(c => c.Action).NotNull());
        }
    }

    public static class AddRolePermissionCommandExtensions
    {
        private static readonly UpdateRolePermissionsCommandValidator validator = new UpdateRolePermissionsCommandValidator();
        public static IEnumerable<ValidationError> Validate(this UpdateRolePermissionsCommand command, Role role, bool throws = false)
        {
            var context = new ValidationContext<UpdateRolePermissionsCommand>(command);
            context.RootContextData.Add(nameof(Role), role);

            return validator.ValidateCommand(context, throws);
        }
    }
}
