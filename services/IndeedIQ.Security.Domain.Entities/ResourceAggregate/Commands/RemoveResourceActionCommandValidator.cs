using FluentValidation;

using IndeedIQ.Common.Domain.Contracts.Exceptions;
using IndeedIQ.Common.Domain.Entities.FluentValidationExtensions;
using IndeedIQ.Common.Domain.Entities.FluentValidationExtensions.CustomValidatos;

using System.Collections.Generic;

namespace IndeedIQ.Security.Domain.Entities.ResourceAggregate.Commands
{
    public class RemoveResourceActionCommandValidator : AbstractDomainCommandValidator<RemoveResourceActionCommand>
    {
        public RemoveResourceActionCommandValidator()
        {
            this.RuleFor(c => c.Action).NotNull();
            this.RuleFor(c => c.Action).ExistsInCollection(nameof(ApplicationResource.AvailableActions)).When(c => c.Action != null);
        }
    }

    public static class RemoveResourceActionCommandExtensions
    {
        private static readonly RemoveResourceActionCommandValidator validator = new RemoveResourceActionCommandValidator();
        public static IEnumerable<ValidationError> Validate(this RemoveResourceActionCommand command,
            ApplicationResource resource, bool throws = false)
        {
            var context = new ValidationContext<RemoveResourceActionCommand>(command);
            context.RootContextData.Add(nameof(ApplicationResource.AvailableActions), resource.AvailableActions);

            return validator.ValidateCommand(context, throws);
        }
    }
}
