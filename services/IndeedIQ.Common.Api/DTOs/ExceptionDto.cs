
using IndeedIQ.Common.Domain.Contracts;
using IndeedIQ.Common.Domain.Contracts.Exceptions;

using System.Collections.Generic;

namespace IndeedIQ.Common.Api.DTOs
{
    public class ExceptionDto
    {
        public string Message { get; internal set; }
        public ExceptionCode ExceptionCode { get; internal set; } = ExceptionCode.UnexpectedException;
        public IEnumerable<ValidationError> ValidationErros { get; internal set; }
    }
}
