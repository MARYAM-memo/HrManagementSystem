
namespace Hr.MVC.Middleware
{
      public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
      {
            private readonly RequestDelegate _next = next;
            private readonly ILogger<RequestLoggingMiddleware> _logger = logger;

            public async Task InvokeAsync(HttpContext context)
            {
                  if (_logger.IsEnabled(LogLevel.Information))
                  {
                        _logger.LogInformation("Request: {Method} {Path}", context.Request.Method, context.Request.Path);
                  }
                  await _next(context);
                  if (_logger.IsEnabled(LogLevel.Information))
                  {
                        _logger.LogInformation("Response: {StatusCode}", context.Response.StatusCode);
                  }
            }
      }
}