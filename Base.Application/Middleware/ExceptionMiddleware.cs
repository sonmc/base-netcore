using Base.Application.Helper;
using Microsoft.Extensions.Localization;


namespace Base.Application.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                var middlewareHelper = new MiddlewareHelper();
                await middlewareHelper.HandleExceptionAsync(httpContext, ex);
            }
        }
    }
}

