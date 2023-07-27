namespace Base.Application.Middleware
{
    public class RouteCaptureMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RouteCaptureMiddleware> _logger;

        public RouteCaptureMiddleware(RequestDelegate next, ILogger<RouteCaptureMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var route = context.Request.Path.Value;
            _logger.LogInformation($"Captured route: {route}");

            await _next(context);
        }
    }
}
