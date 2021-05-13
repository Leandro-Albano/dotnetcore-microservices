using FluentValidation;
using FluentValidation.Validators;

using System;
using System.Collections.Generic;
using System.Linq;

namespace IndeedIQ.Common.Domain.Entities.FluentValidationExtensions.CustomValidatos
{
    public static class ExistsInCollectionValidatorExtension
    {
        public static IRuleBuilderOptions<T, TProperty> ExistsInCollection<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string collectionName)
        {
            return ruleBuilder
                .SetValidator(new ExistsInCollectionValidator<T, TProperty>(collectionName))
                .WithErrorCode(nameof(ExistsInCollectionValidator<T, TProperty>));
        }
    }

    public class ExistsInCollectionValidator<T, TProperty> : PropertyValidator<T, TProperty>
    {
        private readonly string collectionName;

        public ExistsInCollectionValidator(string collectionName)
            => this.collectionName = collectionName;

        public override string Name => throw new NotImplementedException();

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "{PropertyName} does not exist in {CollectionName} collection.";

        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            IEnumerable<object> coll = context.RootContextData[this.collectionName] as IEnumerable<object>;

            if (coll.Contains(value))
                return true;

            context.MessageFormatter.AppendArgument("CollectionName", this.collectionName);

            return false;
        }
    }
}
