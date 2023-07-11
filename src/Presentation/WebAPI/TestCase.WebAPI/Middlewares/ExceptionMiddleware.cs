using System.Net;
using TestCase.Domain.Exceptions;
using TestCase.Application.Common;
using System.Text.Json;

namespace TestCase.WebAPI.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));            
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var result = Result<object>.Failure(exception.Message, (int)HttpStatusCode.InternalServerError);

                if (exception is EntityNotFoundException)
                    result.StatusCode = (int)HttpStatusCode.NotFound;

                _logger.LogError(exception, "An exception occurred in the application.");

                var response = context.Response;
                if (!response.HasStarted)
                {
                    response.ContentType = "application/json";
                    response.StatusCode = result.StatusCode;
                    await response.WriteAsync(JsonSerializer.Serialize(result));
                }
                else
                    _logger.LogWarning("Can't write error response. Response has already started.");
            }
        }
    }
}
