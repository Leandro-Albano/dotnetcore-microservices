using System;

namespace Dynasoft.Common.Domain.Contracts.Exceptions
{
    public class UnexpectedException : DomainException
    {
        public UnexpectedException(string message) : this(message, null) { }
        public UnexpectedException(string message, Exception innerException) : base(ExceptionCode.UnexpectedException, innerException, message)
        {
        }
    }
}
