
using Dynasoft.Common.Domain.Contracts;
using Dynasoft.Common.Domain.Contracts.Exceptions;

using System.Collections.Generic;

namespace Dynasoft.Common.Api.DTOs
{
    public class ExceptionDto
    {
        public string Message { get; internal set; }
        public ExceptionCode ExceptionCode { get; internal set; } = ExceptionCode.UnexpectedException;
        public IEnumerable<ValidationError> ValidationErros { get; internal set; }
    }
}
