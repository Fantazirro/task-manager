using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Api.Middleware
{
    public class ExceptionHandlerMiddleware : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError($"Exception occured: {exception.Message}");

            var errors = new List<string>() { exception.Message };

            switch (exception)
            {
                case BadRequestException _:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case NotFoundException _:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            await httpContext.Response.WriteAsJsonAsync(errors);

            return true;
        }
    }
}