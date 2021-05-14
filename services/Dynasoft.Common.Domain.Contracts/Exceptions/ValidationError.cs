using Dynasoft.Common.Util;

using System;

namespace Dynasoft.Common.Domain.Contracts.Exceptions
{
    public class ValidationError : EquatableObject<ValidationError>
    {
        public string Member { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }

        protected override bool IsEqualTo(ValidationError other)
            => this.Member == other.Member && this.Code == other.Code;

        public override int GetHashCode()
            => HashCode.Combine(this.Member, this.Code);

    }
}
