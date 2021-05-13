using IndeedIQ.Common.Api.DTOs;
using IndeedIQ.Common.Domain.Contracts;
using IndeedIQ.Common.Domain.Contracts.Exceptions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace IndeedIQ.Common.Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        private static readonly Dictionary<ExceptionCode, HttpStatusCode> ExceptionCodeToHttpStatusCodeMap = new Dictionary<ExceptionCode, HttpStatusCode>
        {
            { ExceptionCode.ValidationException, HttpStatusCode.UnprocessableEntity },
            { ExceptionCode.EntityNotFoundException, HttpStatusCode.NotFound },
            { ExceptionCode.UnexpectedException, HttpStatusCode.InternalServerError },
        };

        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    Exception error = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;

                    context.Response.StatusCode = error is DomainException exception
                        ? (int)ExceptionCodeToHttpStatusCodeMap[exception.ExceptionCode]
                        : (int)HttpStatusCode.InternalServerError;

                    ExceptionDto dto = error.ToExceptionDto();

                    context.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(dto, DefaultResponseJsonSerializerOptions.Options));

                });
            });
            return applicationBuilder;
        }

        private static ExceptionDto ToExceptionDto(this Exception exception)
        {
            if (exception is null)
                return null;

            ExceptionDto dto = new ExceptionDto
            {
                Message = exception.Message,
            };

            if (exception is DomainException domainException)
            {
                dto.ExceptionCode = domainException.ExceptionCode;

                if (domainException is DomainValidationException validationException)
                    dto.ValidationErros = validationException.ValidationErrors;
            }

            return dto;
        }
    }
}
