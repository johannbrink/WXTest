using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace JBWooliesXTest.API
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                _logger.LogInformation(
                    "Received HTTP Request {method} {url}{queryString}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Request?.QueryString.Value);
                await _next(context);
            }
            finally
            {
                _logger.LogInformation(
                    "Completed HTTP Request {method} {url}{queryString} => {statusCode}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Request?.QueryString.Value,
                    context.Response?.StatusCode);
            }
        }

    }
}
