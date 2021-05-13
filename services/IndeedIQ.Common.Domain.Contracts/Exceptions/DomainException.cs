using System;

namespace IndeedIQ.Common.Domain.Contracts.Exceptions
{
    public abstract class DomainException : ApplicationException
    {
        public ExceptionCode ExceptionCode { get; set; }

        public DomainException(ExceptionCode code, string message, params object[] messageParams)
            : this(code, null, string.Format(message, messageParams)) { }

        public DomainException(ExceptionCode code, Exception innerException, string message, params object[] messageParams)
            : base(string.Format(message, messageParams), innerException) => this.ExceptionCode = code;
    }
}
