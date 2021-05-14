using System;
using System.Net;

namespace Dynasoft.Security.Application.Client
{
    public class AuthException : ApplicationException
    {
        public AuthException(System.Net.HttpStatusCode statusCode, string message, Exception inner)
            : base(message, inner) => this.StatusCode = statusCode;

        public HttpStatusCode StatusCode { get; }
    }
}
