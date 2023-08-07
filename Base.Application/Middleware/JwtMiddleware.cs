namespace Base.Application.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;   

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next; 
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                bool isAuthorize = CheckAuthorize(context);
                if (!isAuthorize)
                {
                    await _next(context);
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public bool CheckAuthorize(HttpContext context)
        {
            return true;
        }
    }
}
