using FluentValidation;

using IndeedIQ.Common.Domain.Contracts.Exceptions;
using IndeedIQ.Common.Domain.Entities.FluentValidationExtensions;
using IndeedIQ.Common.Domain.Entities.FluentValidationExtensions.CustomValidatos;

using System.Collections.Generic;

namespace IndeedIQ.Security.Domain.Entities.ResourceAggregate.Commands
{
    public class UpdateResourceActionCommandValidator : AbstractDomainCommandValidator<UpdateResourceActionCommand>
    {
        public UpdateResourceActionCommandValidator()
        {
            this.RuleFor(p => p.Action).NotNull();
            this.RuleFor(p => p.Name).NotEmpty();
            this.RuleFor(p => p.Action).ExistsInCollection(nameof(ApplicationResource.AvailableActions)).When(c => c.Action != null);
        }
    }

    public static class UpdateResourceActionCommandExtensions
    {
        private static readonly UpdateResourceActionCommandValidator validator = new UpdateResourceActionCommandValidator();
        public static IEnumerable<ValidationError> Validate(this UpdateResourceActionCommand command, ApplicationResource resource, bool throws = false)
        {
            var ctx = new ValidationContext<UpdateResourceActionCommand>(command);
            ctx.RootContextData.Add(nameof(ApplicationResource.AvailableActions), resource.AvailableActions);
            return validator.ValidateCommand(ctx, throws);
        }
    }
}
