namespace HoneyShop.Infrastructure.Middlewares
{
    public class DenyFrameMiddleware
    {
        private readonly RequestDelegate _next;

        public DenyFrameMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("X-Frame-Options", "DENY");
            await _next(context);
        }
    }
}