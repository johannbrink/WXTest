using System.IO;
using System.Text;
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
                context.Request.EnableBuffering();

                // Leave the body open so the next middleware can read it.
                using var reader = new StreamReader(
                    context.Request.Body,
                    encoding: Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: false,
                    bufferSize: 10,
                    leaveOpen: true);
                var body = await reader.ReadToEndAsync();

                // Reset the request body stream position so the next middleware can read it
                context.Request.Body.Position = 0;
                
                _logger.LogInformation(
                    @"Received HTTP Request {method} {url}{queryString} {body}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Request?.QueryString.Value,
                    body);
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
