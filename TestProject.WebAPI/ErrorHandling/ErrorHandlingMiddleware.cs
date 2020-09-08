using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TestProject.WebAPI.ErrorHandling
{
    internal class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the error handling middleware will not be executed.");
                }
                else
                {
                    await HandleExceptionAsync(context, ex);
                }
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string result;
            context.Response.ContentType = "application/json";

            _logger.LogError("Internal error occured: " + exception.Message);

            if (exception is HttpStatusCodeException)
            {
                HttpStatusCodeException httpStatusCodeException = exception as HttpStatusCodeException;
                context.Response.StatusCode = (int)httpStatusCodeException.StatusCode;
                result = new ErrorDetails
                {
                    Message = exception.Message,
                    StatusCode = context.Response.StatusCode
                }.ToString();
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result = new ErrorDetails
                {
                    Message = "Internal Server Error - please try again later",
                    StatusCode = context.Response.StatusCode
                }.ToString();
            }

            return context.Response.WriteAsync(result);
        }
    }
}
