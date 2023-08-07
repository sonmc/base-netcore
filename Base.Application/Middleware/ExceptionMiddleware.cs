using Base.Application.Helper;
using Microsoft.Extensions.Localization;


namespace Base.Application.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IStringLocalizer<MiddlewareHelper> _localizer;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IStringLocalizer<MiddlewareHelper> localizer)
        {
            _logger = logger;
            _localizer = localizer;
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
                var middlewareHelper = new MiddlewareHelper(_localizer);
                await middlewareHelper.HandleExceptionAsync(httpContext, ex);
            }
        }
    }
}

